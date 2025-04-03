using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//  для сохранения 
public class Dialogue
{
  public string name;

  [TextArea(3, 10)] //максимальное и минимальное количество строк 
  public string[] sentences;
}
