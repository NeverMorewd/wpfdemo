﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Thunisoft.Demo.Data
{
    public sealed class Person:IComparable<Person>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }

        public override string ToString()
        {
            return string.Format("Person(Id = {0}, Name = {1})", Id, Name);
        }

        public Person(long id, string name)
        {
            Id = id;
            Name = name;
        }
        public int CompareTo(Person other)
        {
            //当前实例signflag与other相同
            if (this.Id.CompareTo(other.Id) == 0)
            {
                return 0;
            }
            else
            {
                return this.Id.CompareTo(other.Id) > 0 ? -1 : 1;
            }
        }
    }

    public static class PersonModule
    {
        /// <summary>
        /// List of name automatically generated by http://listofrandomnames.com . Thanks!
        /// </summary>
        static readonly string source =
            @"
1
11
111
2
22
222
a
aa
aaa
克蕾雅
迪妮莎
弗洛拉
普莉西亚
伊斯力
露雪娜
拉花娜
丽芙露
奥菲利亚
伊莉妮
迪妮维
温妮迪
芙罗拉
比茜
中潏
蜚蠊
恶来
弟季胜
孟增
横父
造父
大骆
侄非子
秦侯
秦公伯
曾孙秦仲
秦庄公
秦襄公
秦文公
秦静公
秦宪公
秦出子
秦武公
秦德公
秦宣公
秦成公
秦穆公任好
秦康公
秦共公
秦桓公
秦景公
秦哀公
秦夷公
秦惠公
秦悼公
秦厉共公
秦躁公
秦怀公
秦灵公
秦简公
秦惠公
秦出公
秦献公
秦孝公
秦惠文王
秦武王
秦昭襄王
秦孝文王
秦庄襄王
秦始皇
秦二世
曲灵风
陈玄风
梅超风
陆乘风
武眠风
冯默风
黄药师
段智兴
欧阳锋
洪七公
王重阳
紫衫龙王
白眉鹰王
金毛狮王
青翼蝠王
";

        static readonly IReadOnlyList<Person> allPersons =
            source.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select((name, i) => new Person(i + 1L, name))
            .ToArray();

        public static IReadOnlyList<Person> All
        {
            get { return allPersons; }
        }
    }

    public static class PersonModuleResult
    {
        /// <summary>
        /// List of name automatically generated by http://listofrandomnames.com . Thanks!
        /// </summary>
        static readonly string source =
            @"
1
11
111
2
22
222
a
aa
aaa
克蕾雅
迪妮莎
弗洛拉
普莉西亚
伊斯力
露雪娜
拉花娜
丽芙露
奥菲利亚
伊莉妮
迪妮维
温妮迪
芙罗拉
比茜
中潏
蜚蠊
恶来
弟季胜
孟增
横父
造父
大骆
侄非子
秦侯
秦公伯
曾孙秦仲
秦庄公
秦襄公
秦文公
秦静公
秦宪公
秦出子
秦武公
秦德公
秦宣公
秦成公
秦穆公任好
秦康公
秦共公
秦桓公
秦景公
秦哀公
秦夷公
秦惠公
秦悼公
秦厉共公
秦躁公
秦怀公
秦灵公
秦简公
秦惠公
秦出公
秦献公
秦孝公
秦惠文王
秦武王
秦昭襄王
秦孝文王
秦庄襄王
秦始皇
秦二世
曲灵风
陈玄风
梅超风
陆乘风
武眠风
冯默风
黄药师
段智兴
欧阳锋
洪七公
王重阳
紫衫龙王
白眉鹰王
金毛狮王
青翼蝠王
";
        private static readonly IReadOnlyList<Person> allPersons =
            source.Split(new[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select((name, i) => new Person(i + 1L, name))
            .ToArray();

        public static IReadOnlyList<Person> All
        {
            get { return allPersons; }
        }
    }
}
