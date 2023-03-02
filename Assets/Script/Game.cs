using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public int width;
    public int height;
    public int mineCount;
    [SerializeField] private GameObject mineUI;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject circle;

    private Board board;
    private Cell[,] state;
    private bool firstClick;
    private bool gameover;
    private bool win, lose;
    private bool keyboard;
    private GameObject data;

    private void OnValidate()
    {
        mineCount = Mathf.Clamp(mineCount, 0, width * height);
    }

    private void Awake()
    {
        data = GameObject.Find("GameMaster");
        board = GetComponentInChildren<Board>();
    }

    private void Start()
    {
        height = data.GetComponent<GameData>().GetHeight;
        width = data.GetComponent<GameData>().GetWidth;
        mineCount = data.GetComponent<GameData>().GetMines;
        mineUI.GetComponent<Text>().text = mineCount.ToString();
        NewGame();
    }

    public void NewGame()
    {
        state = new Cell[width, height];
        firstClick = true;
        gameover = false;
        keyboard = data.GetComponent<GameData>().KeyboardMode;

        GenerateCells();
        GenerateMines();
        GenerateNumbers();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -1);
        Camera.main.orthographicSize = (height / 2) + 1;
        board.Draw(state);
    }

    private void GenerateCells()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = Cell.Type.Empty;
                state[x, y] = cell;
            }
        }
    }

    private void GenerateMines()
    {
        for (int i = 0; i < mineCount; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            while (state[x, y].type == Cell.Type.Mine)
            {
                x++;

                if (x >= width)
                {
                    x = 0;
                    y++;

                    if (y >= height)
                    {
                        y = 0;
                    }
                }
            }
            state[x, y].type = Cell.Type.Mine;
        }
    }

    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    continue;
                }

                cell.number = CountMines(x, y);

                if (cell.number > 0)
                {
                    cell.type = Cell.Type.Number;
                }

                //cell.revealed = true;
                state[x, y] = cell;
            }
        }
    }

    private int CountMines(int cellX, int cellY)
    {
        int count = 0;

        for (int adjacentX = -1; adjacentX <= 1; adjacentX++)
        {
            for (int adjacentY = -1; adjacentY <= 1; adjacentY++)
            {
                if (adjacentX == 0 && adjacentY == 0)
                {
                    continue;
                }

                int x = cellX + adjacentX;
                int y = cellY + adjacentY;

                if (GetCell(x, y).type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void Update()
    {
        if (!gameover)
        {
            if (!keyboard)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Flag();
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    Reveal();
                }
            } else if (keyboard)
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    Reveal();
                }
                else if (Input.GetKeyUp(KeyCode.Return))
                {
                    Flag();
                }
            }
        }
        else if (gameover)
        {
            end.SetActive(true);
            if (win)
            {
                winPanel.SetActive(true);
            }
            else if (lose)
            {
                losePanel.SetActive(true);
            }
        }
    }

    private void Flag()
    {
        Vector3 worldPosition = new Vector3();
        if (!keyboard)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (keyboard)
        {
            worldPosition = circle.transform.position;
        }

        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed)
        {
            return;
        }

        cell.flagged = !cell.flagged;
        state[cellPosition.x, cellPosition.y] = cell;
        board.Draw(state);
    }

    private void Flood(Cell cell)
    {
        if (cell.revealed)
        {
            return;
        }
        if (cell.type == Cell.Type.Mine || cell.type == Cell.Type.Invalid)
        {
            return;
        }

        cell.revealed = true;
        state[cell.position.x, cell.position.y] = cell;

        if (cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));

            Flood(GetCell(cell.position.x + 1, cell.position.y + 1));
            Flood(GetCell(cell.position.x + 1, cell.position.y - 1));
            Flood(GetCell(cell.position.x - 1, cell.position.y + 1));
            Flood(GetCell(cell.position.x - 1, cell.position.y - 1));
        }
    }

    private void Reveal()
    {
        Vector3 worldPosition = new Vector3();
        if (!keyboard)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (keyboard)
        {
            worldPosition = circle.transform.position;
        }
        Vector3Int cellPosition = board.tilemap.WorldToCell(worldPosition);
        Cell cell = GetCell(cellPosition.x, cellPosition.y);

        if (cell.type == Cell.Type.Invalid || cell.revealed || cell.flagged)
        {
            return;
        }

        switch (cell.type)
        {
            case Cell.Type.Mine:
                if (firstClick == true)
                {
                    if (GetCell(cell.position.x, cell.position.y).type != Cell.Type.Empty)
                    {
                        NewGame();
                        Reveal();
                    }
                }
                else
                {
                    Explode(cell);
                }
                break;

            case Cell.Type.Empty:
                firstClick = false;
                Flood(cell);
                CheckWinCondition();
                break;

            default:
                if (firstClick == true)
                {
                    if (GetCell(cell.position.x, cell.position.y).type != Cell.Type.Empty)
                    {
                        NewGame();
                        Reveal();
                    }
                }
                else
                {
                    cell.revealed = true;
                    state[cellPosition.x, cellPosition.y] = cell;
                    CheckWinCondition();
                }
                break;
        }

        board.Draw(state);
    }

    private void Explode(Cell cell)
    {
        Debug.Log("Game Over");
        gameover = true;
        lose = true;
        win = false;

        cell.revealed = true;
        cell.exploded = true;
        state[cell.position.x, cell.position.y] = cell;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.revealed = true;
                    state[x, y] = cell;
                }
            }

        }
    }

    private void CheckWinCondition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type != Cell.Type.Mine && !cell.revealed)
                {
                    return;
                }
            }
        }

        Debug.Log("Winner");
        gameover = true;
        win = true;
        lose = false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];

                if (cell.type == Cell.Type.Mine)
                {
                    cell.flagged = true;
                    state[x, y] = cell;
                }
            }
        }
    }

    private Cell GetCell(int x, int y)
    {
        if (IsValid(x, y))
        {
            return state[x, y];
        }
        else
        {
            return new Cell();
        }
    }

    private bool IsValid(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

}