using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkData : MonoBehaviour
{
}

namespace Data{
    [Serializable]
    public struct Card{
        public string id;
        public string name;
        public string url;
    }
}
