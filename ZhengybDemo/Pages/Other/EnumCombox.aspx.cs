﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zhengyb.Bizlogic.BaseEnum;

namespace ZhengybDemo.Pages.Other
{
    /// <summary>
    /// 枚举的相关应用
    /// </summary>
    public partial class EnumCombox : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bind();
            string aa = EnumManager<BusinessBaseEnum.enumCaseRegisterApprovalStatus>.GetEnumValue(1);
            Dictionary<int, string> status = EnumManager<BusinessBaseEnum.enumCaseRegisterApprovalStatus>.GetEnumDictionary();
            string aab = BusinessBaseEnum.enumCaseRegisterApprovalStatus.听证受理.ToString();
        }

        private void Bind()
        {
            EnumManager<BusinessBaseEnum.enumHearingApplyType>.Bind_Enum_Control(this.tingzheng, false);
            EnumManager<BusinessBaseEnum.enumCaseAcceptStatus>.Bind_Enum_Control(this.rdoTest);
            rdoTest.SelectedIndex = 1;
        }
        #region C# Enum,Int,String的互相转换 枚举转换 
        /*Enum为枚举提供基类，其基础类型可以是除 Char 外的任何整型。如果没有显式声明基础类型，则使用 Int32。编程语言通常提供语法来声明由一组已命名的常数和它们的值组成的枚举。

            注意：枚举类型的基类型是除 Char 外的任何整型，所以枚举类型的值是整型值。

            Enum 提供一些实用的静态方法：

            (1)比较枚举类的实例的方法

            (2)将实例的值转换为其字符串表示形式的方法

            (3)将数字的字符串表示形式转换为此类的实例的方法

            (4)创建指定枚举和值的实例的方法。

            举例：enum Colors { Red, Green, Blue, Yellow };

            Enum-->String

            (1)利用Object.ToString()方法：如Colors.Green.ToString()的值是"Green"字符串；

            (2)利用Enum的静态方法GetName与GetNames：

            public static string GetName(Type enumType,Object value)

            public static string[] GetNames(Type enumType)

            例如：Enum.GetName(typeof(Colors),3))与Enum.GetName(typeof(Colors), Colors.Blue))的值都是"Blue"

            Enum.GetNames(typeof(Colors))将返回枚举字符串数组。

            String-->Enum

            (1)利用Enum的静态方法Parse：

            public static Object Parse(Type enumType,string value)

            例如：(Colors)Enum.Parse(typeof(Colors), "Red")

            Enum-->Int

            (1)因为枚举的基类型是除 Char 外的整型，所以可以进行强制转换。

            例如：(int)Colors.Red, (byte)Colors.Green

            Int-->Enum

            (1)可以强制转换将整型转换成枚举类型。

            例如：Colors color = (Colors)2 ，那么color即为Colors.Blue

            (2)利用Enum的静态方法ToObject。

            public static Object ToObject(Type enumType,int value)

            例如：Colors color = (Colors)Enum.ToObject(typeof(Colors), 2)，那么color即为Colors.Blue

            判断某个整型是否定义在枚举中的方法：Enum.IsDefined

            public static bool IsDefined(Type enumType,Object value)

            例如：Enum.IsDefined(typeof(Colors), n))
            */
        #endregion


    }
}