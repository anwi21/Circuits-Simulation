//Anwi Gundavarapu
//CPSC3200: MinMax Circuit
//5/4/2025
//Version 1


using CircuitsInterface;
using MathCircuits;
namespace MinMaxCircuits;

/*
 * Class: MinMax Class
 * -----------------------------------------------------------------
 * Expected State and Conditions:
 * - A client can access all the functionalit of a MathCircuitClass
 * - Once a circuit is frozen, it can be unfrozen using unfreeze().
 * - When constructed, the circuit holds two integers and a supported state
 * - minMax uses the following states in char format: '>' = max state and '<' = min state
 * - If an unsupported state is used, an exception is thrown
 * - The object starts in an unfrozen state, allowing changes to the operator.
 * - Changing the state when a circuit is frozen will result in an exception thrown.
 *   when an illegal operation is attempted, an exception is thrown
 * - The class always returns a valid value as long as the state is supported.
 * - An instance of this circuit can be deep copied to another using DeepCopy
 * - This Circuit allows wiring, where this circuit's output can be wired to a comparison circuit
 * - The circuit must be unwired before wiring and vice versa.
 * - Any invalid state changes result in an exception
 * - A client may use getWiredIndex() to get the inputIndex of its result in its wired CompCircuit.
 *
 * ------------------------------------------------------------------------
 * Relationship between mutators:
 * - freeze() disables setOperator()
 *
 */

public class MinMaxCircuit: MathCircuit
{
    
   
    private char state;

    private MinMaxCircuit() : base()
    {
      
    }
   
    public MinMaxCircuit(int n1, int n2, char stateType) : base(n1, n2,':')
    {
        if (stateType!= '>' && stateType != '<')
        {
            throw new ArgumentException("Invalid state type");
        }
        state = stateType;
    }
   
   
    private MinMaxCircuit clone()
    {
        MinMaxCircuit copy = new MinMaxCircuit();
        copy.setNum1(getNum1());
        copy.setNum2(getNum2());
        copy.state = state;
        if (isFrozen())
        {
            copy.freeze();
        }

        if (isWired())
        {
            copy.wire(getWireIndex());
        }
        return copy;
    }

    public override Circuits DeepCopy()
    {
        return clone();
    }
   

    public override void setOperator(char stateType)
    {
        if (stateType != '>' && stateType != '<')
        {
            throw new ArgumentException("Invalid state type");
        }
      
        state = stateType;
    }

    public override char getOperator()
    {
        return state;
    }


    public override int calculate()
    {
        if (state == '>')
        {
            return getNum1() > getNum2() ? getNum1() : getNum2();
        }
        return getNum1() < getNum2() ? getNum1() : getNum2();
    }
}


/* Implentation Invanriant
 *
 * Design choices:
 * - states are stored as chars, each state is a single char '>' for max and '<' for min
 * - isFrozen is a bool that can flags a circuit as immutable in terms of its operator.
 * - immutability is disabled when a circuit is unfrozen
 * - Constructor is public so that an object cam be initialized by client
 *- Deep Coopy is public which calls private clone to make a deepCopy of obj 
 * - public calculate so a client can calculate and get the result
 * - Calculate returns an int value after calculatiodn, int divison is not handled.
 *
 * bookkeeping:

 * - all data values and flags are private for encapsulation,
 * they cannot be directly accessed by client
 */ 