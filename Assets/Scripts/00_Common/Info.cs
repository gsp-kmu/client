using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSP
{
    public static class Info
    {
        //public const string ip = "http://localhost:8000";
        public const string ip = "http://ec2-43-201-164-245.ap-northeast-2.compute.amazonaws.com:8000";
    }

    public static class http
    {
        private static string ip {get{ return Info.ip; } }
        public static string getDeck { get { return ip + "/getDeck"; } }
        public static string getCard { get { return ip + "/getcard"; } }
        public static string saveDeck { get { return ip + "/savedeck"; } }
        public static string random { get { return ip + "/random"; } }
        public static string register { get { return ip + "/register"; } }
        public static string login { get { return ip + "/login"; } }
    }

    public static class Scene
    {
        public const string title = "00_TitleScene";
        public const string login = "01_LoginScene";
        public const string main = "02_MainScene";
        public const string ingame = "03_GameScene";
        public const string mycard = "04_MyCardScene";
        public const string garcha = "05_CardGachaScene";
    }
}

