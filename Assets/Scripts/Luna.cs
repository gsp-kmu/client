using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luna : Card
{
    static GameObject LunaEffect;

    // Start is called before the first frame update
    void Start()
    {
        LunaEffect = Resources.Load<GameObject>("Prefebs/Effect/LunaEffect");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void BattleCry(Digit digit)
    {

        GameController controller = GameController.GetInstance();

        if (digit == Digit.Ten)
        {
            StartCoroutine(Move(controller.player_ten.transform));

        }
    }

    static IEnumerator Move(Transform start)
    {

        yield return new WaitForSeconds(0.5f);

        Transform card_transform = start.GetChild(start.childCount - 1).GetComponent<Card>().transform;

        Debug.Log(card_transform);

        Destroy(Instantiate(LunaEffect, card_transform), 2.0f);



        GameController controller = GameController.GetInstance();

        while (card_transform.localScale != Vector3.one * 1.2f)
        {
            card_transform.localScale = Vector3.MoveTowards(card_transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 5);

            yield return new WaitForSeconds(0);
        }


        while (card_transform.localScale != Vector3.zero)
        {
            card_transform.localScale = Vector3.MoveTowards(card_transform.localScale, Vector3.zero, Time.deltaTime * 5);

            yield return new WaitForSeconds(0);
        }

        card_transform.parent = controller.player_one.transform;
        card_transform.localPosition = Vector3.zero;

        while (card_transform.localScale != Vector3.one * 1.2f)
        {
            card_transform.localScale = Vector3.MoveTowards(card_transform.localScale, Vector3.one * 1.2f, Time.deltaTime * 5);

            yield return new WaitForSeconds(0);
        }

        while (card_transform.localScale != Vector3.one)
        {
            card_transform.localScale = Vector3.MoveTowards(card_transform.localScale, Vector3.one, Time.deltaTime * 5);

            yield return new WaitForSeconds(0);
        }


    }
}
