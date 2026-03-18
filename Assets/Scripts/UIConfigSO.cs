using UnityEngine;

[CreateAssetMenu(fileName = "NewUIConfig", menuName = "CardGame/Configs/UIConfig")]
public class UIConfigSO : ScriptableObject
{
    private static UIConfigSO _instance;
    public static UIConfigSO Instance {
        get {
            if (_instance == null) 
                _instance = Resources.Load<UIConfigSO>("ScriptableObjects/UIConfigSO");
            return _instance;
        }
    }
    
    public float MouseHoverPopYOffset; // 鼠标虚掩在手牌上时手牌的上浮y轴偏移
    public float MouseHoverZoomInFactor; // 鼠标虚掩在手牌上时手牌的放大倍率
    public float MouseHoverPopAnimationDuration; // 鼠标虚掩在手牌上时手牌弹出放大的动画持续时间;
    public float MouseHoverRecoverAnimationDuration; // 鼠标结束虚掩离开手牌时手牌恢复原状的动画持续时间;
}
