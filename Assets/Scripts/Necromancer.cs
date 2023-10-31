using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class Necromancer : Card
{
    GameObject ghost;
    void Awake()
    {
        if (ghost == null)
            ghost = Resources.Load<GameObject>("Prefebs/Effect/ghost");
    }

    void Update()
    {

    }

    public override void BattleCry(Digit digit)
    {
        StartCoroutine(NecromancerSkill(digit));
    }

    public IEnumerator NecromancerSkill(Digit digit)
    {
        GameController controller = GameController.GetInstance();

        Transform startPos = digit == Digit.One ? controller.player_one.transform : controller.player_ten.transform;
        Transform endPos = digit == Digit.One ? controller.player_ten.transform : controller.player_one.transform;

        List<GameObject> ghosts = new List<GameObject>();

        for (int i = 0; i < startPos.childCount - 1; i++)
            ghosts.Add(Instantiate(ghost, controller.effect_ts));

        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            ghosts[i].GetComponent<SpriteRenderer>().DOFade(1, 0.5f);

            float angle = ((360 / ghosts.Count) * (i + 1) + 90) * Mathf.Deg2Rad;
            Vector3 movePos = new Vector3(Mathf.Cos(angle) * 10, Mathf.Sin(angle) * 10, 0);
            ghosts[i].transform.position = startPos.position;
            ghosts[i].transform.DOMove(movePos, 1);

            ghosts[i].transform.up = movePos - transform.position;
        }

        yield return new WaitForSeconds(1);

        for (int i = 0; i < ghosts.Count; i++)
        {
            ghosts[i].GetComponent<SpriteRenderer>().DOFade(0, 0.5f);

            startPos.GetChild(i).transform.position = ghosts[i].transform.position;
            startPos.GetChild(i).transform.localScale = Vector3.zero;
            startPos.GetChild(i).transform.DOScale(Vector3.one, 0.5f);
            startPos.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 1500;
        }

        yield return new WaitForSeconds(0.5f);

        Card selectCard;
        while (true)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse_point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouse_point = new Vector3(mouse_point.x, mouse_point.y, 0);

                Collider2D hit = Physics2D.OverlapPoint(mouse_point);

                if (!hit)
                    continue;

                if (hit.transform.parent != startPos)
                    continue;

                if (hit.transform == startPos.GetChild(startPos.childCount - 1))
                    continue;

                selectCard = hit.GetComponent<Card>();
                break;
            }

            yield return new WaitForSeconds(0);
        }

        for(int i = 0; i < ghosts.Count; i++)
        {
            if (selectCard.transform == startPos.GetChild(i))
            {
                selectCard.transform.DOMove(endPos.position, 0.5f);
                selectCard.transform.DOScale(Vector3.one * 2, 0.5f);
            }
            else
            {
                startPos.GetChild(i).DOScale(Vector3.zero, 0.0f);
            }
        }

        yield return new WaitForSeconds(0.5f);

        selectCard.transform.parent = endPos;
        selectCard.transform.localPosition = Vector3.zero;

        startPos.GetComponent<BattleFieldCards>().OrganizeCard();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = Vector3.zero;
            transform.GetChild(i).localScale = Vector3.one * 2;
        }

        yield return new WaitForSeconds(0);
    }
}