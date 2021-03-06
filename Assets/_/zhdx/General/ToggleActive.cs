﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace General
    {
        public class ToggleActive : MonoBehaviour
        {
            public void Toggle()
            {
                gameObject.SetActive(!gameObject.activeInHierarchy);
            }
        }
    }
}
