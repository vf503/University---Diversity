$(document).ready(function () {
    $(function () {
        $("#KinSlideshow").KinSlideshow({
            moveStyle: "left",
            intervalTime: 3,
            mouseEvent: "mouseover",
            isHasTitleBar: false,
            isHasTitleFont: false,
            titleFont: { TitleFont_size: 14, TitleFont_color: "#FF0000" },
            btn: { btn_bgColor: "#666666", btn_bgHoverColor: "#07c5f9",
                btn_fontColor: "#ffffff", btn_fontHoverColor: "#ffffff", btn_fontFamily: "Verdana",
                btn_borderColor: "#999999", btn_borderHoverColor: "#d4e7ed",
                btn_borderWidth: 1, btn_bgAlpha: 0.7
            }
        });
    });
})
