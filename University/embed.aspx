<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="embed.aspx.cs" Inherits="colleges.embed" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title></title>
    <style type="text/css" media="all">
        .d1
        {
            margin: 0px auto;
            width: 1002px;
            height: auto;
            overflow: hidden;
            white-space: nowrap;
        }
        .d2
        {
            margin: 0px auto;
            background-color: #FF9933;
        }
        .div2
        {
            width: auto;
            height: auto;
            font-size: 12px;
            float: left;
            overflow: hidden;
        }
        .div2 span
        {
            padding: 0px 2px;
        }
        a:link, a:visited
        {
            color: #3F78CF;
            text-decoration: none;
        }
        a:hover
        {
            color: #FF00CC;
            text-decoration: underline;
        }
        .search_btn
        {
            border-bottom-style: none;
            text-align: center;
            border-right-style: none;
            width: 59px;
            border-top-style: none;
            background: url(../img/btn_login.jpg) no-repeat;
            float: left;
            height: 18px;
            border-left-style: none;
            cursor: pointer;
        }
        .search_btna
        {
            border-bottom-style: none;
            text-align: center;
            border-right-style: none;
            width: 59px;
            border-top-style: none;
            background: url(../img/dg_info.jpg) no-repeat;
            float: left;
            height: 18px;
            border-left-style: none;
            cursor: pointer;
        }
        .w
        {
            margin-top: 10px;
            width: 980px;
            display: inline-block;
            position: relative;
        }
        .wsy
        {
            margin-top: 10px;
            width: 980px;
            float: left;
            margin-left: 12px;
        }
        .y
        {
            width: 980px;
            float: left;
        }
        .note_all
        {
            width: 1002px;
            background: url(../img/head_bg.jpg) repeat-x left top;
            float: left;
            height: 20px;
            overflow: hidden;
        }
        .d1
        {
            margin: 0px auto;
            line-height: 20px;
            height: 20px;
            padding-left: 6px;
            padding-right: 6px;
            white-space: nowrap;
            float: left;
            overflow: hidden;
        }
        .note
        {
            float: left;
        }
        .note .b
        {
            padding-left: 15px;
            background: url(../img/quar.jpg) no-repeat 4px 6px;
            float: left;
            height: 20px;
            color: #054a81;
        }
        .note SPAN
        {
            display: inline-block;
            float: left;
            height: 20px;
        }
        .imgborder
        {
            border-bottom: #dff8fd 1px solid;
            border-left: #dff8fd 1px solid;
            padding-bottom: 1px;
            padding-left: 1px;
            padding-right: 1px;
            border-top: #dff8fd 1px solid;
            border-right: #dff8fd 1px solid;
            padding-top: 1px;
        }
        .box1
        {
            border: #dbdbdb 1px solid;
            width: 230px;
            margin-bottom: 10px;
            float: left;
            margin-right: 12px;
        }
        .box1 .mt
        {
            position: relative;
            line-height: 28px;
            background: url(../img/tit_1.jpg) repeat-x;
            height: 28px;
            border-top: #006fc0 3px solid;
        }
        .box1 .mt .more
        {
            position: absolute;
            width: 34px;
            background: url(../img/more.jpg) no-repeat;
            height: 11px;
            top: 8px;
            right: 5px;
        }
        .box1 .mt h2
        {
            padding-left: 10px;
            background: url(../img/icon2.gif) no-repeat 0 4px;
        }
        .box1 .mc
        {
            position: relative;
            padding-bottom: 13px;
            padding-left: 13px;
            padding-right: 13px;
            height: 115px;
            padding-top: 13px;
            background-color: #fffbf5;
            overflow: hidden;
        }
        .box1 .mc B
        {
            padding-bottom: 5px;
            display: block;
        }
        .box1 .mc P
        {
            line-height: 21px;
        }
        .box1 .xiangxi
        {
            position: absolute;
            bottom: 8px;
            right: 8px;
        }
        .box1 .mc UL LI
        {
            line-height: 22px;
            padding-left: 10px;
            background: url(../img/list_icon.jpg) no-repeat 0px center;
        }
        .ul_2
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 8px;
            padding-left: 23px;
            width: 940px;
            padding-right: 0px;
            zoom: 1;
            float: left;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 8px;
        }
        .ul_2 LI
        {
            line-height: 27px;
            padding-left: 12px;
            width: 110px;
            padding-right: 10px;
            background: url(../img/right_jiantou.gif) no-repeat 0px center;
            float: left;
            font-size: 14px;
        }
        .move
        {
            width: 1400px;
            background: url(../img/movebg.jpg) repeat-x;
            height: 155px;
        }
        .move DL
        {
            padding-bottom: 0px;
            padding-left: 8px;
            padding-right: 8px;
            float: left;
            padding-top: 20px;
        }
        .move DL DT
        {
        }
        .move DL DD
        {
            text-align: center;
        }
        .ul_3
        {
            padding-left: 30px;
            zoom: 1;
            overflow: hidden;
        }
        .ul_3 LI
        {
            width: 310px;
            float: left;
        }
        .box2
        {
            border: #dbdbdb 1px solid;
            padding-bottom: 1px;
            padding-left: 1px;
            width: 968px;
            padding-right: 1px;
            zoom: 1;
            margin-left: 12px;
            overflow: hidden;
            padding-top: 1px;
        }
        .box2 .mc1
        {
            padding-left: 10px;
            width: 1050px;
        }
        .box2 .mt .tit
        {
            position: relative;
            margin-bottom: 10px;
            background: url(../img/jianbian_bg.jpg) no-repeat right center;
            height: 29px;
        }
        .box2 .mt H2 .span0
        {
            position: absolute;
            text-align: center;
            line-height: 37px;
            width: 105px;
            display: block;
            background: url(../img/tit_cur_blue.jpg) no-repeat;
            height: 37px;
            color: #fff;
            top: -3px;
            left: 6px;
        }
        .box2 .mt H2 .span1
        {
            position: absolute;
            text-align: center;
            line-height: 37px;
            width: 105px;
            display: block;
            background: url(../img/tit_cur_yellow.jpg) no-repeat;
            height: 37px;
            color: #fff;
            top: -3px;
            left: 6px;
        }
        .box22
        {
            width: 220px;
            float: left;
            height: 166px;
            margin-right: 23px;
        }
        .box22 H2
        {
            color: #033598;
        }
        .box22 .mt1
        {
            border-bottom: #dbdbdb 1px solid;
            position: relative;
            border-left: #dbdbdb 1px solid;
            line-height: 23px;
            padding-left: 35px;
            width: 183px;
            background: url(../img/icno_tit.jpg) no-repeat;
            height: 23px;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
        }
        .box22 .mt
        {
            border-bottom: #a68e44 1px solid;
            position: relative;
            line-height: 23px;
            padding-left: 8px;
            background: url(../img/jiantou_down.gif) no-repeat left center;
            height: 23px;
        }
        .box22 .more
        {
            position: absolute;
            top: 1px;
            right: 6px;
        }
        .box22 .mc
        {
            padding-bottom: 8px;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 8px;
        }
        .box22 .mc UL
        {
            clear: both;
            overflow: hidden;
        }
        .box22 .mc UL.ul11
        {
            height: 52px;
        }
        .box22 .mc UL.ul22
        {
        }
        .box22 .mc UL.ul11 LI
        {
            padding-left: 10px;
            width: 60px;
            background: url(../img/icon_jiantou.gif) no-repeat left center;
            float: left;
        }
        .box22 .mc UL.ul22 LI
        {
            line-height: 22px;
            padding-left: 10px;
            background: url(../img/list_icon.jpg) no-repeat 0px center;
        }
        .ul11 a, .ul11 a:visited
        {
            color: #000;
            text-decoration: none;
        }
        .ul11 a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        .box22 .mc B
        {
            padding-bottom: 5px;
            padding-left: 0px;
            padding-right: 0px;
            display: block;
            padding-top: 2px;
        }
        .box22 .mc P
        {
            line-height: 21px;
        }
        .ul_4
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 10px;
            padding-left: 18px;
            width: 952px;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 10px;
        }
        .ul_4 LI
        {
            padding-left: 10px;
            width: 78px;
            padding-right: 7px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .ul_5
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 10px;
            padding-left: 18px;
            width: 952px;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 10px;
        }
        .ul_5 LI
        {
            line-height: 26px;
            padding-left: 10px;
            width: 99px;
            padding-right: 7px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .ul_6
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 10px;
            padding-left: 18px;
            width: 952px;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 10px;
        }
        .ul_6 LI
        {
            line-height: 26px;
            padding-left: 10px;
            width: 47px;
            padding-right: 2px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .ul_7
        {
            padding-bottom: 20px;
            padding-left: 18px;
            zoom: 1;
            overflow: hidden;
            padding-top: 6px;
        }
        .ul_7 LI
        {
            padding-bottom: 0px;
            padding-left: 12px;
            padding-right: 12px;
            float: left;
            padding-top: 0px;
        }
        .content
        {
            width: 990px;
            float: left;
            margin-left: 12px;
        }
        .MyMarqueeX
        {
            width: 98%;
            height: 125px;
            margin-left: 9px;
            overflow: hidden;
        }
        .wrapper
        {
            margin: 0px auto;
            width: 990px;
        }
        .hidden
        {
            display: none;
        }
        .clumn_3
        {
            margin: 5px 0px 0px 7px;
            margin: 0px 0px 0px 7px\9;
            display: inline-block;
            _left: -10px;
            position: relative;
            width: 960px;
            background: url(images/v_bg_1.gif) repeat-x;
            height: 200px;
            overflow: hidden;
        }
        .clumn_3 UL
        {
            float: left;
        }
        .clumn_3 LI
        {
            padding-bottom: 5px;
            padding-left: 10px;
            width: 110px;
            padding-right: 10px;
            float: left;
            padding-top: 5px;
        }
        .clumn_3 LI IMG
        {
            width: 110px;
            float: left;
            height: 90px;
        }
        .clumn_3 LI P
        {
            text-align: center;
            width: 110px;
            float: left;
            padding-top: 1px;
        }
        .d2
        {
            background-color: #ff9933;
            margin: 0px auto;
        }
        .div2
        {
            width: auto;
            float: left;
            height: 26px;
            font-size: 12px;
            overflow: hidden;
        }
        .div2 SPAN
        {
            padding-bottom: 0px;
            padding-left: 2px;
            padding-right: 2px;
            padding-top: 0px;
        }
        .box22_a
        {
            width: 220px;
            float: left;
            height: 166px;
            margin-right: 23px;
        }
        .box22_a H2
        {
            color: #033598;
        }
        .box22_a .mt1
        {
            border-bottom: #dbdbdb 1px solid;
            position: relative;
            border-left: #dbdbdb 1px solid;
            line-height: 23px;
            padding-left: 35px;
            width: 183px;
            background: url(../img/icno_tit.jpg) no-repeat;
            height: 23px;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
        }
        .box22_a .mt
        {
            border-bottom: #a68e44 1px solid;
            position: relative;
            line-height: 23px;
            padding-left: 8px;
            background: url(../img/jiantou_down.gif) no-repeat left center;
            height: 23px;
        }
        .box22_a .more
        {
            position: absolute;
            top: 1px;
            right: 6px;
        }
        .box22_a .mc
        {
            padding-bottom: 8px;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 8px;
        }
        .box22_a .mc UL
        {
            clear: both;
            overflow: hidden;
        }
        .box22_a .mc UL.ul11
        {
            height: 52px;
        }
        .box22_a .mc UL.ul22
        {
        }
        .box22_a .mc UL.ul11 LI
        {
            padding-left: 10px;
            width: 60px;
            background: url(../img/icon_jiantou.gif) no-repeat left center;
            float: left;
        }
        .box22_a .mc UL.ul22 LI
        {
            line-height: 25px;
            padding-left: 10px;
            background: url(../img/list_icon.jpg) no-repeat 0px center;
        }
        .box22_a .mc B
        {
            padding-bottom: 5px;
            padding-left: 0px;
            padding-right: 0px;
            display: block;
            padding-top: 2px;
        }
        .box22_a .mc P
        {
            line-height: 24px;
        }
        .box22_b
        {
            width: 220px;
            float: left;
            height: 166px;
            margin-right: 23px;
        }
        .box22_b H2
        {
            color: #033598;
        }
        .box22_b .mt1
        {
            border-bottom: #dbdbdb 1px solid;
            position: relative;
            border-left: #dbdbdb 1px solid;
            line-height: 23px;
            padding-left: 35px;
            width: 183px;
            background: url(../img/icno_tit.jpg) no-repeat;
            height: 23px;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
        }
        .box22_b .mt
        {
            border-bottom: #a68e44 1px solid;
            position: relative;
            line-height: 23px;
            padding-left: 8px;
            background: url(../img/jiantou_down.gif) no-repeat left center;
            height: 23px;
        }
        .box22_b .more
        {
            position: absolute;
            top: 1px;
            right: 6px;
        }
        .box22_b .mc
        {
            padding-bottom: 8px;
            padding-left: 3px;
            padding-right: 3px;
            padding-top: 8px;
        }
        .box22_b .mc UL
        {
            clear: both;
            overflow: hidden;
        }
        .box22_b .mc UL.ul11
        {
            height: 52px;
        }
        .box22_b .mc UL.ul22
        {
        }
        .box22_b .mc UL.ul11 LI
        {
            padding-left: 10px;
            width: 80px;
            background: url(../img/icon_jiantou.gif) no-repeat left center;
            float: left;
        }
        .box22_b .mc UL.ul22 LI
        {
            line-height: 25px;
            padding-left: 10px;
            background: url(../img/list_icon.jpg) no-repeat 0px center;
        }
        .box22_b .mc B
        {
            padding-bottom: 5px;
            padding-left: 0px;
            padding-right: 0px;
            display: block;
            padding-top: 2px;
        }
        .box22_b .mc P
        {
            line-height: 24px;
        }
        .pic_list
        {
            margin: 10px 0px 10px 12px;
            width: 990px;
            float: left;
            _display: inline;
        }
        .pic_list UL
        {
            float: left;
        }
        .pic_list UL LI
        {
            padding-right: 18px;
            float: left;
        }
        .zw_style
        {
            float: left;
            margin-left: 12px;
            _display: inline;
        }
        .isua
        {
            display: block;
            width: 965px;
            left: 19px;
            position: relative;
        }
        .x
        {
            margin-top: 10px;
            width: 980px;
            float: left;
            margin-left: 12px;
            _display: inline;
        }
        .xa
        {
            margin-top: 10px;
            width: 960px;
            margin-bottom: 10px;
            float: left;
            margin-left: 12px;
            _display: inline;
        }
        .list_all
        {
            width: 1002px;
            clear: both;
        }
        .list
        {
            padding-bottom: 15px;
            margin-top: 10px;
            padding-left: 15px;
            width: 958px;
            padding-right: 15px;
            background: url(../img/list_bg_10.gif) repeat-y 2px top;
            float: left;
            padding-top: 5px;
        }
        .list_L
        {
            width: 223px;
            float: left;
            overflow: auto;
        }
        .list_R
        {
            width: 723px;
            float: right;
        }
        .sideBar_1
        {
            line-height: 24px;
            padding-left: 13px;
            width: 210px;
            background: url(../img/list_1.gif) no-repeat;
            float: left;
            height: 24px;
            color: #172c95;
            font-size: 14px;
            font-weight: bold;
        }
        .sideBar_2
        {
            border-bottom: #b7d7f0 1px solid;
            border-left: #b7d7f0 1px solid;
            padding-bottom: 8px;
            padding-left: 8px;
            width: 205px;
            padding-right: 8px;
            float: left;
            border-right: #b7d7f0 1px solid;
            padding-top: 8px;
        }
        .sideBar_2 LI
        {
            margin: 5px 0px;
            padding-left: 20px;
            width: 185px;
            background: url(../images/list_2_1.gif) no-repeat;
            float: left;
            height: 22px;
            font-size: 14px;
            padding-top: 6px;
        }
        .sideBar_2 .over_4
        {
            background: url(../images/list_2.gif) no-repeat;
            color: #172d93;
            font-weight: bold;
        }
        .sideBar_3
        {
            border-bottom: #b7d7f0 1px solid;
            border-left: #b7d7f0 1px solid;
            padding-bottom: 8px;
            padding-left: 8px;
            width: 205px;
            padding-right: 8px;
            float: left;
            border-right: #b7d7f0 1px solid;
            padding-top: 8px;
        }
        .sideBar_3 LI
        {
            line-height: 26px;
            padding-left: 15px;
            width: 87px;
            background: url(../images/dian_1.gif) no-repeat 5px 12px;
            float: left;
            font-size: 14px;
        }
        .ad_L_1
        {
            width: 223px;
            float: left;
            padding-top: 5px;
        }
        .nowPosition_list
        {
            border-bottom: #b5d7f3 1px solid;
            border-left: #b5d7f3 1px solid;
            line-height: 21px;
            padding-left: 20px;
            width: 701px;
            background: url(../img/list_4.gif) repeat-x;
            float: left;
            height: 21px;
            font-size: 14px;
            border-top: #b5d7f3 1px solid;
            border-right: #b5d7f3 1px solid;
        }
        .nowPosition_list EM
        {
            padding-left: 10px;
            background: url(../images/arrow_5.gif) no-repeat left 7px;
            float: left;
        }
        .list_word
        {
            padding-bottom: 0px;
            padding-left: 30px;
            width: 635px;
            padding-right: 0px;
            float: left;
            padding-top: 20px;
        }
        .list_word LI
        {
            padding-bottom: 10px;
            line-height: 20px;
            width: 635px;
            float: left;
        }
        .list_word_p_1
        {
            font-size: 14px;
        }
        .list_word_p_1 EM
        {
            padding-bottom: 0px;
            padding-left: 8px;
            padding-right: 8px;
            color: #666666;
            padding-top: 0px;
        }
        .list_word_p_1 A
        {
            color: #028003;
        }
        .list_word_p_1 STRONG A
        {
            color: #0201cb;
            font-size: 16px;
            font-weight: normal;
            text-decoration: underline;
        }
        .list_word_p_2
        {
            font-size: 13px;
        }
        .list_word_1
        {
            padding-bottom: 25px;
            padding-left: 35px;
            width: 635px;
            float: left;
        }
        .list_word_1 LI
        {
            line-height: 24px;
            padding-left: 10px;
            width: 635px;
            background: url(../images/arrow_7.gif) no-repeat left 9px;
            float: left;
        }
        .list_word_1 UL
        {
            width: 500px;
            float: left;
            padding-top: 25px;
        }
        .list_word_1 LI A
        {
            color: #0b5aa7;
            font-size: 14px;
        }
        .list_word_1 LI EM
        {
            padding-bottom: 0px;
            padding-left: 8px;
            padding-right: 8px;
            color: #878787;
            padding-top: 0px;
        }
        .search_result_1
        {
            padding-bottom: 13px;
            margin-top: 3px;
            padding-left: 20px;
            width: 958px;
            padding-right: 10px;
            background: #fbfae6;
            float: left;
            padding-top: 13px;
        }
        .search_result_1 INPUT
        {
            margin-left: 12px;
            vertical-align: middle;
        }
        .search_result_1 IMG
        {
            margin-left: 12px;
            vertical-align: middle;
            cursor: pointer;
        }
        .search_result_1 .search_inp_2
        {
            border-bottom: #cecece 1px solid;
            border-left: #cecece 1px solid;
            line-height: 20px;
            width: 200px;
            height: 20px;
            margin-left: 2px;
            border-top: #cecece 1px solid;
            border-right: #cecece 1px solid;
        }
        .search_result_2
        {
            text-align: center;
            padding-bottom: 13px;
            padding-left: 20px;
            width: 958px;
            padding-right: 10px;
            background: #fbfae6;
            float: left;
            padding-top: 13px;
        }
        .search_result_2 INPUT
        {
            margin-left: 12px;
            vertical-align: middle;
        }
        .search_result_2 IMG
        {
            margin-left: 12px;
            vertical-align: middle;
            cursor: pointer;
        }
        .search_result_2 .search_inp_2
        {
            border-bottom: #cecece 1px solid;
            border-left: #cecece 1px solid;
            line-height: 20px;
            width: 200px;
            height: 20px;
            margin-left: 2px;
            border-top: #cecece 1px solid;
            border-right: #cecece 1px solid;
        }
        .search_result_3
        {
            text-align: center;
            line-height: 30px;
            margin-top: 10px;
            width: 988px;
            background: #f3f3f3;
            float: left;
            font-size: 14px;
        }
        .search_result_4
        {
            padding-bottom: 0px;
            padding-left: 141px;
            width: 705px;
            padding-right: 141px;
            float: left;
            padding-top: 0px;
        }
        .search_paging_1
        {
            text-align: center;
            width: 705px;
            float: left;
            font-size: 14px;
            padding-top: 10px;
        }
        .search_paging_1 A
        {
            margin: 0px 3px;
            font-size: 13px;
        }
        .search_list
        {
            width: 705px;
            float: left;
            padding-top: 20px;
        }
        .search_list LI
        {
            padding-bottom: 20px;
            width: 705px;
            float: left;
        }
        .search_list LI STRONG
        {
            padding-bottom: 0px;
            line-height: 24px;
            padding-left: 10px;
            width: 685px;
            padding-right: 10px;
            background: #f0f1f3;
            float: left;
            font-size: 14px;
            padding-top: 0px;
        }
        .search_list LI STRONG A
        {
            color: #103289;
        }
        .search_list LI SPAN
        {
            float: right;
            color: #333333;
            font-weight: bold;
        }
        .search_list_time
        {
            color: #054a98;
        }
        .search_list LI P
        {
            position: relative;
            padding-bottom: 0px;
            line-height: 20px;
            padding-left: 10px;
            width: 685px;
            padding-right: 10px;
            float: left;
            padding-top: 0px;
        }
        .search_list LI P EM
        {
            position: absolute;
            bottom: 2px;
            right: 10px;
            font-weight: bold;
        }
        .search_list LI P EM A
        {
            color: #054a98;
        }
        .paging
        {
            border-bottom: #e0e0e0 1px solid;
            border-left: #e0e0e0 1px solid;
            width: 721px;
            background: url(../img/paging_1.gif) repeat-x;
            float: left;
            height: 27px;
            border-top: #e0e0e0 1px solid;
            border-right: #e0e0e0 1px solid;
        }
        .paging .fl
        {
            padding-left: 25px;
            padding-top: 5px;
        }
        .paging .fl INPUT
        {
            vertical-align: middle;
        }
        .inp_1
        {
            border-bottom: #c6d6e3 1px solid;
            border-left: #c6d6e3 1px solid;
            padding-bottom: 0px;
            line-height: 16px;
            margin: 0px 4px;
            padding-left: 3px;
            width: 20px;
            padding-right: 3px;
            height: 16px;
            border-top: #c6d6e3 1px solid;
            border-right: #c6d6e3 1px solid;
            padding-top: 0px;
        }
        .btn_1
        {
            border-bottom-style: none;
            border-right-style: none;
            width: 25px;
            border-top-style: none;
            background: url(../images/paging_2.gif) no-repeat;
            height: 18px;
            color: #0c70b6;
            border-left-style: none;
            cursor: pointer;
        }
        .paging .fr
        {
            padding-right: 20px;
        }
        .paging .fr IMG
        {
            margin: 0px 4px;
        }
        .paging_btn_1
        {
            line-height: 27px;
            margin: 0px 4px;
            padding-left: 10px;
        }
        .paging_btn_2
        {
            line-height: 27px;
            margin: 0px 4px;
            padding-left: 10px;
        }
        .paging_btn_3
        {
            line-height: 27px;
            margin: 0px 4px;
            padding-left: 10px;
        }
        .paging_btn_4
        {
            line-height: 27px;
            margin: 0px 4px;
            padding-left: 10px;
        }
        .g_j_search
        {
            padding-bottom: 20px;
            margin: 60px 94px;
            padding-left: 80px;
            width: 640px;
            padding-right: 80px;
            display: inline;
            background: #fcfafb;
            float: left;
            padding-top: 20px;
        }
        .g_j_search H2
        {
            text-align: center;
            padding-bottom: 30px;
            color: #214c90;
            font-size: 18px;
        }
        .g_j_search_1
        {
            border-bottom: #fee238 1px solid;
            text-align: center;
            border-left: #fee238 1px solid;
            line-height: 24px;
            width: 568px;
            display: inline;
            margin-bottom: 10px;
            background: #fffbe5;
            float: left;
            color: #ff0000;
            margin-left: 70px;
            border-top: #fee238 1px solid;
            border-right: #fee238 1px solid;
        }
        .g_j_search LI
        {
            line-height: 20px;
            width: 640px;
            float: left;
            padding-top: 15px;
        }
        .g_j_search LI LABEL
        {
            text-align: left;
            width: 70px;
            float: left;
            color: #234994;
            font-size: 14px;
        }
        .g_j_inp_1
        {
            border-bottom: #e4e2e3 1px solid;
            border-left: #e4e2e3 1px solid;
            line-height: 18px;
            width: 568px;
            float: left;
            height: 18px;
            border-top: #e4e2e3 1px solid;
            border-right: #e4e2e3 1px solid;
        }
        .g_j_inp_2
        {
            border-bottom: #e4e2e3 1px solid;
            border-left: #e4e2e3 1px solid;
            line-height: 18px;
            margin: 0px 10px;
            width: 110px;
            height: 18px;
            border-top: #e4e2e3 1px solid;
            border-right: #e4e2e3 1px solid;
        }
        .g_j_search LI P
        {
            text-align: center;
            padding-top: 10px;
        }
        .g_j_btn_1
        {
            border-bottom-style: none;
            border-right-style: none;
            margin: 0px 60px;
            width: 70px;
            border-top-style: none;
            background: url(../images/search_btn_5.gif) no-repeat;
            height: 23px;
            border-left-style: none;
            cursor: pointer;
        }
        .g_j_btn_2
        {
            border-bottom-style: none;
            border-right-style: none;
            margin: 0px 60px;
            width: 70px;
            border-top-style: none;
            background: url(../images/search_btn_6.gif) no-repeat;
            height: 23px;
            border-left-style: none;
            cursor: pointer;
        }
        .edit_position
        {
            line-height: 23px;
            margin-top: 5px;
            padding-left: 20px;
            width: 968px;
            margin-bottom: 15px;
            background: url(../images/edit_1.gif) repeat-x;
            float: left;
            height: 23px;
            font-size: 14px;
        }
        .edit_position P
        {
            padding-left: 10px;
            background: url(../images/edit_2.gif) no-repeat left 9px;
        }
        .edit
        {
            padding-left: 5px;
            width: 958px;
            padding-right: 25px;
            float: left;
            color: #4f4f4f;
        }
        .edit A
        {
            color: #4f4f4f;
        }
        .edit_L
        {
            border-bottom: #ebe9f6 1px solid;
            border-left: #ebe9f6 1px solid;
            padding-bottom: 10px;
            padding-left: 10px;
            width: 670px;
            padding-right: 10px;
            float: left;
            border-top: #ebe9f6 1px solid;
            border-right: #ebe9f6 1px solid;
            padding-top: 10px;
        }
        .article
        {
            padding-bottom: 20px;
            padding-left: 45px;
            width: 580px;
            padding-right: 45px;
            float: left;
            padding-top: 20px;
        }
        .article_1
        {
            width: 580px;
            float: left;
        }
        .article_1 H2
        {
            border-bottom: #d8d8d8 1px solid;
            text-align: center;
            padding-bottom: 10px;
            color: #1a4476;
            font-size: 20px;
            font-weight: bold;
        }
        .article_1 SPAN
        {
            text-align: center;
            display: block;
            color: #4d4d4d;
            padding-top: 15px;
        }
        .article_2
        {
            border-bottom: #e7ebf6 1px solid;
            border-left: #e7ebf6 1px solid;
            padding-bottom: 10px;
            line-height: 22px;
            margin: 30px 0px;
            padding-left: 10px;
            width: 558px;
            padding-right: 10px;
            background: #f7f8fc;
            float: left;
            color: #1a436f;
            border-top: #e7ebf6 1px solid;
            border-right: #e7ebf6 1px solid;
            padding-top: 10px;
        }
        .article_3
        {
            padding-bottom: 30px;
            line-height: 24px;
            width: 580px;
            float: left;
            font-size: 14px;
            padding-top: 10px;
        }
        .about_list
        {
            width: 670px;
            float: left;
        }
        .about_list STRONG
        {
            border-bottom: #cce1f2 1px solid;
            border-left: #cce1f2 1px solid;
            line-height: 23px;
            padding-left: 15px;
            width: 653px;
            background: url(../images/edit_3.gif) repeat-x;
            float: left;
            color: #243f74;
            font-size: 14px;
            border-top: #cce1f2 1px solid;
            border-right: #cce1f2 1px solid;
        }
        .about_list UL
        {
            padding-bottom: 20px;
            padding-left: 20px;
            width: 630px;
            padding-right: 20px;
            float: left;
            padding-top: 20px;
        }
        .about_list LI
        {
            line-height: 24px;
            padding-left: 10px;
            width: 620px;
            background: url(../images/edit_2.gif) no-repeat left 9px;
            float: left;
            color: #282828;
            font-size: 14px;
        }
        .about_list LI A
        {
            color: #282828;
        }
        .about_list LI EM
        {
            padding-bottom: 0px;
            padding-left: 20px;
            padding-right: 20px;
            font-size: 12px;
            padding-top: 0px;
        }
        .edit_R
        {
            width: 260px;
            float: right;
        }
        .more_list
        {
            width: 260px;
            float: left;
        }
        .more_list STRONG
        {
            border-bottom: #cce1f2 1px solid;
            border-left: #cce1f2 1px solid;
            line-height: 23px;
            padding-left: 15px;
            width: 243px;
            background: url(../images/edit_3.gif) repeat-x;
            float: left;
            color: #243f74;
            font-size: 14px;
            border-top: #cce1f2 1px solid;
            border-right: #cce1f2 1px solid;
        }
        .more_list UL
        {
            padding-bottom: 10px;
            padding-left: 15px;
            width: 230px;
            padding-right: 15px;
            float: left;
            padding-top: 10px;
        }
        .more_list LI
        {
            line-height: 28px;
            padding-left: 10px;
            width: 220px;
            background: url(../images/dian_1.gif) no-repeat left 12px;
            float: left;
            font-size: 14px;
        }
        .list_paging
        {
            padding-left: 15px;
            width: 245px;
            float: left;
            font-size: 14px;
            padding-top: 10px;
        }
        .list_paging A
        {
            margin: 0px 2px;
            font-size: 12px;
        }
        .webfx-tree-container
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font: icon;
            word-wrap: break-word;
            white-space: nowrap;
            padding-top: 0px;
        }
        .webfx-tree-containerc A
        {
            color: #000;
        }
        .webfx-tree-container A:visited
        {
            color: #000;
        }
        .webfx-tree-item
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font: icon;
            white-space: nowrap;
            color: black;
            padding-top: 0px;
        }
        .webfx-tree-item A
        {
            padding-bottom: 0px;
            padding-left: 2px;
            padding-right: 2px;
            color: #000;
            margin-left: 2px;
            font-size: 9pt;
            padding-top: 2px;
        }
        .webfx-tree-item A:active
        {
            padding-bottom: 0px;
            padding-left: 2px;
            padding-right: 2px;
            color: #000;
            margin-left: 2px;
            font-size: 9pt;
            padding-top: 2px;
        }
        .webfx-tree-item A:hover
        {
            padding-bottom: 0px;
            padding-left: 2px;
            padding-right: 2px;
            color: #000;
            margin-left: 2px;
            font-size: 9pt;
            padding-top: 2px;
        }
        .webfx-tree-item A
        {
            color: #000;
            text-decoration: none;
            padding-top: 2px;
        }
        .webfx-tree-item A:hover
        {
            color: #ff7000;
            text-decoration: none;
        }
        .webfx-tree-item A:active
        {
            background: #ff9400;
            color: #fff;
            font-weight: bold;
            text-decoration: none;
        }
        .webfx-tree-item IMG
        {
            border-right-width: 0px;
            display: inline;
            border-top-width: 0px;
            border-bottom-width: 0px;
            vertical-align: middle;
            border-left-width: 0px;
        }
        .webfx-tree-icon
        {
        }
        .webfx-tree-item A.selected
        {
            background: #ff9400;
            color: #fff;
            font-weight: bold;
        }
        .webfx-tree-item A.selected-inactive
        {
            background: #ff9400;
            color: #fff;
            font-weight: bold;
        }
        .webfx-tree-item A.selected-inactive:hover
        {
            background: #ff9400;
            color: #ffffff;
            font-weight: bold;
        }
        .xtree_title
        {
            font-size: 10.5pt;
            font-weight: bold;
        }
        .abouta_list
        {
            margin: 30px 0px 0px;
            width: 800px;
            float: left;
        }
        .abouta_list STRONG
        {
            border-bottom: #cce1f2 1px solid;
            border-left: #cce1f2 1px solid;
            line-height: 23px;
            padding-left: 15px;
            width: 800px;
            background: url(../images/edit_3.gif) repeat-x;
            float: left;
            color: #243f74;
            font-size: 14px;
            border-top: #cce1f2 1px solid;
            border-right: #cce1f2 1px solid;
        }
        .abouta_list UL
        {
            padding-bottom: 20px;
            padding-left: 0px;
            width: 800px;
            padding-right: 0px;
            float: left;
            padding-top: 20px;
        }
        .abouta_list LI
        {
            line-height: 24px;
            padding-left: 10px;
            width: 800px;
            font-family: "????";
            background: url(../images/edit_2.gif) no-repeat left 9px;
            float: left;
            color: #282828;
            font-size: 14px;
        }
        .abouta_list LI A
        {
            font-family: "????";
            color: #282828;
            font-size: 14px;
            font-weight: normal;
        }
        .abouta_list LI A:visited
        {
            font-family: "????";
            color: #282828;
            text-decoration: none;
        }
        .abouta_list LI A:link
        {
            font-family: "????";
        }
        .abouta_list LI A:active
        {
            font-family: "????";
            color: #000;
            text-decoration: underline;
        }
        .abouta_list LI A:hover
        {
            font-family: "????";
            color: #000;
            text-decoration: underline;
        }
        .abouta_list LI EM
        {
            float: right;
            font-size: 12px;
        }
        .t_l
        {
            text-align: center;
            color: #4d4d4d;
            font-size: 12px;
        }
        .now_list
        {
            padding-bottom: 0px;
            padding-left: 0px;
            padding-right: 0px;
            float: right;
            font-size: 14px;
            padding-top: 5px;
        }
        .tab_a
        {
            border-bottom: #d8d8d8 1px solid;
            text-align: center;
            height: 40px;
            color: #1c4468;
            font-size: 20px;
            font-weight: bold;
        }
        .tab_b
        {
            text-align: center;
            line-height: 50px;
            height: 50px;
            color: #4e4e4e;
            font-size: 12px;
            font-weight: normal;
        }
        .tab_c
        {
            border-bottom: #e7ebf6 1px solid;
            border-left: #e7ebf6 1px solid;
            padding-bottom: 10px;
            line-height: 1.6;
            background-color: #f7f8fc;
            padding-left: 10px;
            padding-right: 10px;
            color: #1a436f;
            font-size: 12px;
            border-top: #e7ebf6 1px solid;
            border-right: #e7ebf6 1px solid;
            padding-top: 10px;
        }
        .tab_d
        {
            padding-bottom: 30px;
            line-height: 1.6;
            padding-left: 0px;
            padding-right: 0px;
            color: #333;
            font-size: 14px;
            padding-top: 30px;
        }
        .tab_e
        {
            background-image: url(../images/edit_3.gif);
            border-bottom: #cce1f2 1px solid;
            border-left: #cce1f2 1px solid;
            color: #243f74;
            font-size: 14px;
            border-top: #cce1f2 1px solid;
            font-weight: bold;
            border-right: #cce1f2 1px solid;
        }
        .tab_f
        {
            padding-bottom: 10px;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 10px;
        }
        .tab_g
        {
            text-align: center;
        }
        .link_a
        {
            line-height: 1.6;
            color: #282828;
            font-size: 14px;
        }
        A.link_a:visited
        {
            color: #282828;
            text-decoration: none;
        }
        A.link_a:link
        {
            color: #282828;
            text-decoration: none;
        }
        A.link_a:active
        {
            color: #000;
            text-decoration: underline;
        }
        A.link_a:hover
        {
            color: #000;
            text-decoration: underline;
        }
        .menu_two
        {
            background-image: url(../img/list_bg_9.gif);
            width: 1002px;
            float: left;
            height: 36px;
        }
        .menu_two UL
        {
            padding-left: 10px;
        }
        .menu_two UL LI
        {
            padding-bottom: 0px;
            line-height: 33px;
            padding-left: 15px;
            padding-right: 15px;
            background: url(../img/icon_jiantou.gif) no-repeat left center;
            float: left;
            color: #000;
            padding-top: 0px;
        }
        .menu_two UL LI A:hover
        {
            color: #282828;
            text-decoration: none;
        }
        .ul_4a
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 10px;
            padding-left: 18px;
            width: 925px;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 10px;
        }
        .ul_4a LI
        {
            padding-left: 10px;
            width: 78px;
            padding-right: 4px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .ul_5a
        {
            border: #dbdbdb 1px solid;
            display: block;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            padding: 10px 0 10px 18px;
        }
        .ul_5a LI
        {
            line-height: 26px;
            padding-left: 10px;
            width: 99px;
            padding-right: 7px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .ul_6a
        {
            border-bottom: #dbdbdb 1px solid;
            border-left: #dbdbdb 1px solid;
            padding-bottom: 10px;
            padding-left: 18px;
            width: 925px;
            zoom: 1;
            float: left;
            font-size: 14px;
            overflow: hidden;
            border-top: #dbdbdb 1px solid;
            border-right: #dbdbdb 1px solid;
            padding-top: 10px;
        }
        .ul_6a LI
        {
            line-height: 26px;
            padding-left: 10px;
            width: 47px;
            padding-right: 2px;
            background: url(../img/right_jiantou.gif) no-repeat left center;
            float: left;
        }
        .button_bg
        {
            background-image: url(../img/btn_login.jpg);
            text-align: center;
            line-height: 18px;
            width: 59px;
            background-repeat: no-repeat;
            height: 18px;
            color: #fff;
            font-size: 12px;
        }
        .w2
        {
            width: 980px;
            margin-top: 0px;
            float: left;
            margin-bottom: -10px;
        }
        .ul_1
        {
            border: #dbdbdb 1px solid;
            float: left;
            margin-left: 12px;
            margin-left: 0px\9;
            padding-left: 20px;
            width: 680px;
            height: 98px;
            font-size: 14px;
            padding-top: 13px;
        }
        .ul_1 A
        {
            padding-left: 6px;
            width: 14%;
            display: inline-block;
            background: url(../img/list_icon.jpg) no-repeat 0px 7px;
            height: 30px;
        }
        .ul_1 a, .ul_1 a:visited
        {
            color: #000;
            text-decoration: none;
        }
        .ul_1 a:hover
        {
            color: #3f78cf;
            text-decoration: underline;
        }
        .ul_12
        {
            display: block;
            margin-left: 12px;
            margin-left: 0px\9;
            padding-left: 20px;
            height: 98px;
            font-size: 14px;
            padding-top: 13px;
        }
        .ul_12 a
        {
            padding-left: 8px;
            width: 12.5%;
            display: inline-block;
            background: url(../img/list_icon.jpg) no-repeat 0px 7px;
            height: 30px;
        }
        .ul_12 a, .ul_12 a:visited
        {
            color: #007ecc;
            text-decoration: none;
        }
        .ul_12 a:hover
        {
            color: #1252b3;
            text-decoration: underline;
        }
        .liebiao1
        {
            margin: 10px auto 0 auto;
        }
        .liebiao1 .td
        {
            font-size: 14px;
            padding: 5px 0 5px 8px;
            padding: 5px 0 5px 8px\9;
            background: url(../img/icon1.gif) no-repeat;
            background-position: 0px 14px;
            background-position: 0px 14px\9;
        }
        .liebiao1 .w
        {
            width: 45px;
        }
        .liebiao1 .td a, .liebiao1 .td a:visited
        {
            color: #2e4099;
            text-decoration: none;
        }
        .liebiao1 .td a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        .map
        {
            float: right;
            right: 5px;
            width: 225px;
            position: relative;
            text-align: left;
            border: #a3b8cc 0px solid;
            padding: 10px;
            border-left: #b8cfe5 1px solid;
        }
        .pic_list2
        {
            width: 702px;
            margin: 20px 0px 0px 12px;
            margin: 25px 0px 0px 12px\9;
            _margin: 25px 0px 0px 6px;
            float: left;
        }
        .pic_list2 li
        {
            float: left;
            display: inline-block;
        }
        .pic_list2 .mr
        {
            margin-right: 24px;
        }
        .pic_list2 img
        {
            border: #dbdbdb 1px solid;
            width: 155px;
            height: 70px;
        }
        .fgwsub
        {
            display: block;
            padding-left: 40px;
        }
        .fgwsub li
        {
        }
        .fgwsub li a
        {
            background: url(../img/list_icon.jpg) no-repeat 0px 4px;
            padding-left: 7px;
            font-size: 13px;
            font-weight: bold;
        }
        .fgwsub li a, .fgwsub li a:visited
        {
            color: #737373;
            text-decoration: none;
        }
        .fgwsub li a:hover
        {
            color: #3d8ccc;
            text-decoration: underline;
        }
        .line1
        {
            display: block;
            height: 1px;
            background-color: #b8cfe5;
            overflow-y: hidden;
            margin-left: 12px;
        }
        .line2
        {
            display: block;
            width: 1002px;
            float: left;
            height: 1px;
            background-color: #dbdbdb;
            overflow-y: hidden;
            margin-top: 12px;
            margin-bottom: 10px;
        }
        .show_scroll
        {
            overflow: hidden;
            display: inline-block;
            width: 964px;
            cursor: hand;
            margin: 10px 0px 0 0px;
        }
        .show_scroll_pics
        {
            padding-right: 10px;
            font-size: 9pt;
        }
        .links_list
        {
            margin: 15px 0 5px 10px;
            display: inline-table;
        }
        *
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        BODY
        {
            background-image: url(../img/all_bg.gif);
            padding-bottom: 0px;
            line-height: 1.5;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-family: "宋体";
            background-position: center 50%;
            font-size: 12px;
            padding-top: 0px;
        }
        UL
        {
            padding-bottom: 0px;
            list-style-type: none;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        LI
        {
            padding-bottom: 0px;
            list-style-type: none;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            padding-top: 0px;
        }
        IMG
        {
            border-right-width: 0px;
            border-top-width: 0px;
            border-bottom-width: 0px;
            border-left-width: 0px;
        }
        H1
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        H2
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        H3
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        H4
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        H5
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        H6
        {
            padding-bottom: 0px;
            margin: 0px;
            padding-left: 0px;
            padding-right: 0px;
            font-size: 14px;
            padding-top: 0px;
        }
        .fr
        {
            float: right;
        }
        .fl
        {
            float: left;
        }
        a:visited
        {
            color: #000;
            text-decoration: none;
        }
        a:link
        {
            color: #000;
            text-decoration: none;
        }
        a:active
        {
            color: #1452cc;
            text-decoration: underline;
        }
        a:hover
        {
            color: #1452cc;
            text-decoration: underline;
        }
        .more
        {
        }
        a.more:visited
        {
            color: #8c8c8c;
            text-decoration: none;
        }
        a.more:link
        {
            color: #8c8c8c;
            text-decoration: none;
        }
        a.more:active
        {
            color: #000;
            text-decoration: underline;
        }
        a.more:hover
        {
            color: #000;
            text-decoration: underline;
        }
        .container
        {
            margin: 0px auto;
            width: 1002px;
        }
        .head
        {
            width: 100%;
            float: left;
            overflow: hidden;
        }
        .head .head_top
        {
            line-height: 32px;
            background: url(../img/headtop_bg.jpg) repeat-x;
            height: 32px;
            overflow: hidden;
        }
        .head .head_top .login
        {
            padding-left: 15px;
        }
        .head .head_top .login .inp_l
        {
            line-height: 18px;
            width: 70px;
            height: 18px;
            margin-right: 12px;
        }
        .head .head_top .headtop_rgt A
        {
            padding-bottom: 0px;
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 0px;
        }
        .loginbar
        {
            overflow: hidden;
        }
        .logo
        {
            float: left;
        }
        .search
        {
            float: right;
            overflow: hidden;
            display: inline-block;
            margin-right: 12px;
        }
        .tab_search
        {
            margin-top: 9px;
            float: left;
            display: inline-block;
        }
        .help
        {
            float: left;
            display: inline-block;
            margin: 40px 0 0 8px;
        }
        .tab_search .ul_tab
        {
            _float: left;
            line-height: 24px;
            background: url(../img/tab_ul_bg.jpg) repeat-x center bottom;
            height: 24px;
            clear: both;
            overflow: hidden;
        }
        .tab_search .ul_tab LI
        {
            text-align: center;
            line-height: 24px;
            width: 72px;
            float: left;
            height: 24px;
            font-size: 14px;
        }
        .tab_search .ul_tab LI.cur
        {
            background: url(../img/tab_cur.jpg) no-repeat center center;
            font-weight: bold;
        }
        .tab_search .search_all
        {
            border-bottom: #dadada 1px solid;
            border-left: #dadada 1px solid;
            background-color: #fafafa;
            padding-left: 10px;
            width: 320px;
            height: 40px;
            border-top: #dadada 1px;
            border-right: #dadada 1px solid;
            padding-top: 10px;
        }
        .tab_search .search_all .search_k
        {
            line-height: 24px;
            padding-left: 30px;
            width: 200px;
            background: url(../img/search_bg.jpg) #fff no-repeat 7px center;
            height: 24px;
            margin-right: 10px;
        }
        .menu
        {
            line-height: 33px;
            margin-top: 1px;
            width: 100%;
            background: url(../img/menu_bg.jpg) repeat-x;
            height: 33px;
        }
        .menu UL LI
        {
            background: url(../img/line.jpg) no-repeat right center;
            float: left;
            color: #fff;
        }
        .menu a, .menu a:visited
        {
            color: #fff;
            text-decoration: none;
        }
        .menu a:hover
        {
            color: #000;
            text-decoration: underline;
        }
        .menu UL LI A
        {
            text-align: center;
            padding-bottom: 0px;
            padding-left: 37px;
            padding-right: 37px;
            display: block;
            float: left;
            color: #fff;
            font-size: 14px;
            font-weight: bold;
            padding-top: 0px;
        }
        .menu UL LI A.cur
        {
            display: block;
            background: url(../img/menu_cur.gif) no-repeat center bottom;
            color: #000;
        }
        .menu UL LI A:hover
        {
            display: block;
            background: url(../img/menu_cur.gif) no-repeat center bottom;
            color: #000;
        }
        .menu UL LI.date
        {
            padding-left: 55px;
            background: none transparent scroll repeat 0% 0%;
            color: #fff;
        }
        .foot
        {
            width: 1002px;
            background: url(../img/footbg.jpg) repeat-x bottom;
            float: left;
            height: 100px;
            border-top: #9b9b9b 1px solid;
        }
        .foot P
        {
            text-align: center;
            padding-top: 25px;
        }
        .foot .p2
        {
            line-height: 40px;
            margin-left: 224px;
            padding-top: 5px;
        }
        .foot .p2 .red
        {
            color: red;
        }
        .foot .p2 .tubiao
        {
            width: 22px;
            display: inline-block;
            background: url(../img/safe_icon.jpg) no-repeat center center;
            float: left;
            height: 40px;
        }
        .foot .p2 SPAN
        {
            display: inline-block;
            float: left;
        }
        /*///////////////clearfix ///////////////*/.clearfix:after
        {
            content: ".";
            display: block;
            clear: both;
            visibility: hidden;
            line-height: 0;
            height: 0;
        }
        .clearfix
        {
            display: inline-block;
        }
        html[xmlns] .clearfix
        {
            display: block;
        }
        * html .clearfix
        {
            height: 1%;
        }
        .clear
        {
            clear: both;
        }
        /*  */.spot
        {
        }
        .spot a:link
        {
            font-size:14px;
            font-weight:bold;
            color: #000;
            text-decoration: underline;
        }
        .spot a:visited
        {
        font-size:14px;
            font-weight:bold;
            color: #000;
            text-decoration: none;
        }
        .spot a:hover
        {
        font-size:14px;
            font-weight:bold;
            color: #000000;
            text-decoration: underline;
        }
        .spot a:active
        {
        font-size:14px;
            font-weight:bold;
            color: #000;
            text-decoration: none;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function Marquee(inits) {
            this.MyMarquees = new Array();
            var _o = this;
            var _i = inits;

            if (_i.obj == undefined) return;
            _o.mode = _i.mode == undefined ? 'x' : _i.mode;
            _o.mName = this.getMarqueeName(_i.name);
            _o.mObj = this.$(_i.obj);
            _o.speed = _i.speed == undefined ? 30 : _i.speed; //速度		
            _o.autoStart = _i.autoStart == undefined ? true : _i.autoStart;
            _o.movePause = _i.movePause == undefined ? true : _i.movePause;

            _o.mDo = null;
            _o.pause = false;

            _o.init = function() {
                if ((_o.mObj.scrollWidth <= _o.mObj.offsetWidth && _o.mode == 'x') || (_o.mObj.scrollHeight <= _o.mObj.offsetHeight && _o.mode == 'y')) return;

                this.MyMarquees.push(_o.mName);

                _o.mObj.innerHTML = _o.mode == 'x' ? (
			'<table width="100%" border="0" align="left" cellpadding="0" cellspace="0">' +
			'	<tr>' +
			'		<td id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</td>' +
			'		<td id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</td>' +
			'	</tr>' +
			'</table>'
		) : (
			'<div id="MYMQ_' + _o.mName + '_1">' + _o.mObj.innerHTML + '</div>' +
			'<div id="MYMQ_' + _o.mName + '_2">' + _o.mObj.innerHTML + '</div>'
		);


                _o.mObj1 = this.$('MYMQ_' + _o.mName + '_1');
                _o.mObj2 = this.$('MYMQ_' + _o.mName + '_2');
                _o.mo1Width = _o.mObj1.scrollWidth;
                _o.mo1Height = _o.mObj1.scrollHeight;


                if (_o.autoStart) _o.start();
            };


            _o.start = function() {
                _o.mDo = setInterval((_o.mode == 'x' ? _o.moveX : _o.moveY), _o.speed);
                if (_o.movePause) {
                    _o.mObj.onmouseover = function() { _o.pause = true; }
                    _o.mObj.onmouseout = function() { _o.pause = false; }
                }
            }


            _o.stop = function() {
                clearInterval(_o.mDo)
                _o.mObj.onmouseover = function() { }
                _o.mObj.onmouseout = function() { }
            }


            _o.moveX = function() {
                if (_o.pause) return;
                var left = _o.mObj.scrollLeft;
                if (left == _o.mo1Width) {
                    _o.mObj.scrollLeft = 0;
                } else if (left > _o.mo1Width) {
                    _o.mObj.scrollLeft = left - _o.mo1Width;
                } else {
                    _o.mObj.scrollLeft++;
                }
            };


            _o.moveY = function() {
                if (_o.pause) return;
                var top = _o.mObj.scrollTop;
                if (top == _o.mo1Height) {
                    _o.mObj.scrollTop = 0;
                } else if (top > _o.mo1Height) {
                    _o.mObj.scrollTop = top - _o.mo1Height;
                } else {
                    _o.mObj.scrollTop++;
                }
            };

            _o.init();
        }
        Marquee.prototype.getMarqueeName = function(mName) {
            var name = mName == undefined ? this.RandStr(5) : mName;
            var myNames = ',' + this.MyMarquees.join(',') + ',';

            while (myNames.indexOf(',' + name + ',') != -1) {
                name = RandStr(5);
            }
            return name;
        }
        Marquee.prototype.RandStr = function(n, u) {
            var tmStr = "abcdefghijklmnopqrstuvwxyz0123456789";
            var Len = tmStr.length;
            var Str = "";
            for (i = 1; i < n + 1; i++) {
                Str += tmStr.charAt(Math.random() * Len);
            }
            return (u ? Str.toUpperCase() : Str);
        };
        Marquee.prototype.$ = function(element) {
            return typeof (element) == 'object' ? element : document.getElementById(element);
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">

    <div class="clumn_3">
       <div style="height:20px;">
       <div style="float:left; padding:0 10px 0 0px; background:white;"><img src="images/EmbedTitle.jpg" /></div>
       <div style="float:right; padding:0 5px 0 5px; background:white;"><a href="EmbedIndex.aspx" target="_blank"><img src="images/EmbedMore.jpg" /></a></div>
       </div>
        <div id="demo" class="show_scroll">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td id="demo1" align="middle">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tbody>
                                    <asp:ListView ID="EmbedList" runat="server">
                                        <ItemTemplate>
                                            <td class="show_scroll_pics">
                                                <a href="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"beta.html")%>"
                                                    title="<%#Eval("Title")%>" target="_blank">
                                                    <img src="<%#colleges.DataQuery.GetCoursePicPath(Eval("ArticleGUID").ToString(),url,"001.jpg")%>"
                                                        width="160" height="120" /><br />
                                                    <%#colleges.DataProcessing.SubstringText(Eval("Title").ToString(),12)%></a>
                                            </td>
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <tr align="middle">
                                                <asp:Panel ID="itemPlaceholder" runat="server">
                                                </asp:Panel>
                                            </tr>
                                        </LayoutTemplate>
                                    </asp:ListView>
                                </tbody>
                            </table>
                        </td>
                        <td width="0" id="demo2">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
       <div style="float:left; padding:0 10px 0 0px; background:white;"></div>
       <div style="float:right; padding:13px 5px 0 5px; background:white;" class="spot"><a href="SpotIndex.aspx" target="_blank">最新会议</a></div>
        <script>
            var speed = 46//速度数值越大速度越慢
            demo2.innerHTML = demo1.innerHTML
            function Marquee() {
                if (demo2.offsetWidth - demo.scrollLeft <= 0)
                    demo.scrollLeft -= demo1.offsetWidth
                else {
                    demo.scrollLeft++
                }
            }
            var MyMar = setInterval(Marquee, speed)
            demo.onmouseover = function() { clearInterval(MyMar) }
            demo.onmouseout = function() { MyMar = setInterval(Marquee, speed) }
        </script>

    </div>
    </form>
</body>
</html>
