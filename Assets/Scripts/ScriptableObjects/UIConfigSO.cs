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
    
    /* 卡牌动画相关参数 */
    public float MouseHoverPopYOffset; // 鼠标虚掩在手牌上时手牌的上浮y轴偏移
    public float MouseHoverZoomInFactor; // 鼠标虚掩在手牌上时手牌的放大倍率
    public float MouseHoverPopAnimationDuration; // 鼠标虚掩在手牌上时手牌弹出放大的动画持续时间;
    public float MouseHoverRecoverAnimationDuration; // 鼠标结束虚掩离开手牌时手牌恢复原状的动画持续时间;
    public float MouseDraggingZoomInFactor; // 当拖拽卡牌时卡牌的放大倍率
    public float MouseDraggingZoomInAnimationDuration; // 点击手牌拖拽时卡牌放大的动画持续时间
    public float MouseDraggingRecoverAnimationDuration; // 手牌拖拽结束时卡牌恢复原状的动画持续时间
    public float CardLayoutAnimationDuration; // 洗牌过程中单张牌到达手牌指定位置的动画持续时间
    public float CardDraggingOnChessBoardAlpha; // 当在棋盘上拖动卡牌时卡牌的透明度
    
    /* 卡牌排列相关参数 */
    public float CardLayoutSpacing; // 当牌位于手牌中时，不同牌之间的间隔参数
    public float CardLayoutHeightOffset; // 令手牌呈现阶梯状的不同相邻卡牌之间的y轴偏移
}
