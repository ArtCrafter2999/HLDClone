using Player;
using UnityEngine;
using Zenject;

public class Medkit : MonoBehaviour
{
    private PlayerVariables _variables;
    [Inject]
    private void Construct(PlayerVariables variables)
    {
        _variables = variables;
    }
    public void GiveMedkit()
    {
        _variables.Medkits++;
        if (_variables.Medkits > _variables.MaxMedkits) 
            _variables.Medkits = _variables.MaxMedkits;
    }
}