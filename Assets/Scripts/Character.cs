using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    
    
    [SerializeField] public byte color;

    public enum TypeOfCharacter
    {
        Knight,
        Elephant,
        Pawn,
        King,
        Queen,
        Bishop
    }

    [SerializeField]  TypeOfCharacter typeOfCharacter;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public TypeOfCharacter GetTypeOfCharacter()
    {
        return typeOfCharacter;
    }
}
