using UnityEngine;

public class Invaders : MonoBehaviour
{
    public Invader[] prefab = new Invader[5];

    private int row = 5;
    private int col = 11;

    private float speed = 1f;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.right;

    public Missile missilePrefab;

    private void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), 1, 1); //Hur ofta ska den skjuta iv�g missiler
    }

    //Skapar sj�lva griden med alla invaders.
    public void CreateInvaderGrid()
    {
        // Check if invaders already exist to avoid duplicating
        if (transform.childCount > 0)
        {
            // Optional: Clear existing children if you want to reset completely
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Create the grid
        for (int r = 0; r < row; r++)
        {
            float width = 2.5f * (col - 1);
            float height = 2.5f * (row - 1);

            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2.5f * r) + centerOffset.y, 0f);

            for (int c = 0; c < col; c++)
            {
                Invader tempInvader = Instantiate(prefab[r], transform);
                Vector3 position = rowPosition;
                position.x += 2.5f * c;
                tempInvader.transform.localPosition = position;
            }
        }
    }

    public void ResetInvaders()
    {
        // If there are no active invaders, recreate the grid
        if (transform.childCount == 0)
        {
            CreateInvaderGrid();
        }

        direction = Vector3.right;
        transform.position = initialPosition;

        // Activate all invaders
        foreach (Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    //Skjuter slumpm�ssigt iv�g en missil.
    void MissileAttack()
    {
        int nrOfInvaders = GetInvaderCount();

        if (nrOfInvaders == 0)
        {
            return;
        }

        foreach (Transform invader in transform)
        {

            if (!invader.gameObject.activeInHierarchy) //om en invader �r d�d ska den inte kunna skjuta...
                continue;


            // increases the randomnes for bullets based on waves (adds chance for double bullet as well)
            float rand = UnityEngine.Random.value;

            float _target = 0.1f + (GameObject.Find("GameManager").GetComponent<GameManager>().wave * 0.1f);
            _target = Mathf.Clamp(_target, 0.2f, 0.9f);
            float _double = 0f + (GameObject.Find("GameManager").GetComponent<GameManager>().wave * 0.085f);
            _double = Mathf.Clamp(_double, 0f, 0.4f);

            if (rand < _target)
            {
                rand = UnityEngine.Random.value;
                if (rand < _double)
                {
                    Missile missile_1 = Instantiate(missilePrefab, invader.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                    missile_1.GetComponent<Missile>().otherDirection = 1;

                    Missile missile_2 = Instantiate(missilePrefab, invader.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                    missile_2.GetComponent<Missile>().otherDirection = -1;
                }
                else
                {
                    Instantiate(missilePrefab, invader.position, Quaternion.identity);
                }
                break;
            }
        }

    }

    //Kollar hur m�nga invaders som lever
    public int GetInvaderCount()
    {
        int nr = 0;

        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }

    //Flyttar invaders �t sidan
    void Update()
    {
        // we make some movement here manipulated by waves
        float speed = 1f + (GameObject.Find("GameManager").GetComponent<GameManager>().wave * 0.2f);
        speed = Mathf.Clamp(speed, 1f, 3f);
        transform.position += speed * Time.deltaTime * direction;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) //Kolla bara invaders som lever
                continue;

            if (direction == Vector3.right && invader.position.x >= rightEdge.x - 1f)
            {
                AdvanceRow();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1f)
            {
                AdvanceRow();
                break;
            }
        }
    }
    //Byter riktning och flytter ner ett steg.
    void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }

    public void IncreaseSpeed()
    {
        speed += 2f;
    }
}
