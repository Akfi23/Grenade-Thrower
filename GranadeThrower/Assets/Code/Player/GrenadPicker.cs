using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GrenadPicker : MonoBehaviour
{
	[SerializeField] protected float pickedRadius;
	[SerializeField] protected LayerMask targetMask;
	[SerializeField] protected Transform _thrower;

	[SerializeField] protected List<GameObject> redGrenades = new List<GameObject>();
	[SerializeField] protected List<GameObject> blueGrenades = new List<GameObject>();

	public event UnityAction<Vector3> PickedUpGrenade;

	public event UnityAction<int> RedGrenadesCount;
	public event UnityAction<int> BlueGrenadesCount;


	protected IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	protected void FindVisibleTargets()
	{
		Collider[] grenadeInViewRadius = Physics.OverlapSphere(transform.position, pickedRadius, targetMask);
		
		for (int i = 0; i < grenadeInViewRadius.Length; i++)
		{
			GameObject target = grenadeInViewRadius[i].gameObject;

            if (target.TryGetComponent<RedGrenade>(out RedGrenade redGrenade))
                AddToList(target, redGrenades);
			RedGrenadesCount?.Invoke(redGrenades.Count);

            if (target.TryGetComponent<BlueGrenade>(out BlueGrenade blueGrenade))
                AddToList(target, blueGrenades);
			BlueGrenadesCount?.Invoke(blueGrenades.Count);
		}
	}

    protected void AddToList(GameObject grenade,List<GameObject> grList)
	{ 
        grenade.SetActive(false);
        grList.Add(grenade);
		PickedUpGrenade?.Invoke(grenade.GetComponent<Grenade>().GrenadePos);
    }

	protected bool TryGetEnemy(out GameObject result,List<GameObject> grList)
	{
		result = grList.First(enemy => enemy.activeSelf == false);
		return result != null;
	}

	protected void RemoveFromList(GameObject gameObject, List<GameObject> gameObjects) 
	{
		gameObjects.Remove(gameObject);
		RedGrenadesCount?.Invoke(redGrenades.Count);
		BlueGrenadesCount?.Invoke(blueGrenades.Count);
	}
}
