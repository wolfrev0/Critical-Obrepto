using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public static Minimap instance;
    public Image pfEnemyIcon;
    public Image pfHpIcon;
    public Image pfAmmoIcon;
    public Image pfSpeedIcon;
    List<KeyValuePair<Transform, RectTransform>> enemies = new List<KeyValuePair<Transform, RectTransform>>();

    void Awake()
    {
        instance = this;
    }

    public void Remove(Transform t)
    {
        foreach (var p in enemies)
            if (p.Key == t)
            {
                enemies.Remove(p);
                Destroy(p.Value.gameObject);
                break;
            }
    }

    public void AddEnemy(Transform t)
    {
        Image icon = Instantiate(pfEnemyIcon);
        icon.transform.SetParent(transform);
        enemies.Add(new KeyValuePair<Transform, RectTransform>(t, icon.rectTransform));
    }

    public void AddHp(Transform t)
    {
        Image icon = Instantiate(pfHpIcon);
        icon.transform.SetParent(transform);
        enemies.Add(new KeyValuePair<Transform, RectTransform>(t, icon.rectTransform));
    }

    public void AddAmmo(Transform t)
    {
        Image icon = Instantiate(pfAmmoIcon);
        icon.transform.SetParent(transform);
        enemies.Add(new KeyValuePair<Transform, RectTransform>(t, icon.rectTransform));
    }

    public void AddSpeed(Transform t)
    {
        Image icon = Instantiate(pfSpeedIcon);
        icon.transform.SetParent(transform);
        enemies.Add(new KeyValuePair<Transform, RectTransform>(t, icon.rectTransform));
    }

    void Update()
    {
        foreach (var p in enemies)
        {
            var playerPos = PlayerHandler.instance.transform.position;
            var toKey = p.Key.transform.position - playerPos;
            toKey = Quaternion.Euler(0, -PlayerHandler.instance.transform.eulerAngles.y, 0) * toKey;
            p.Value.anchoredPosition = new Vector2(toKey.x, toKey.z) * 4;
        }
    }
}