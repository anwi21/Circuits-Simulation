//Anwi Gundavarapu
//CPSC3200: MathCircuit Class
//5/4/2025
//Version 3



using CircuitsInterface;
namespace MathCircuits;

/*
 * Class: MathCircuit
 * -----------------------------------------------------------------
 * Expected State and Conditions:
 * - Once a circuit is frozen, it can be unfrozen using unfreeze().
 * - When constructed, the circuit holds two integers and a supported operator.
 * - Supported operators are: '+','-','/','*','&','%'
 * - If an unsupported operator is used, an exception is thrown
 * - The object starts in an unfrozen state, allowing changes to the operator.
 * - Once frozen, the arithemtic operator cannot be changed.
 * - Changing the operator when a circuit is frozen will result in an exception thrown.
 *   when an illegal operation is attempted, an exception is thrown
 * - Division by 0 is not allowed, an exception is thrown when an illegal calculation is attempted
 * - The class always returns a valid value as long as the operator is a supported operator
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

public class MathCircuit: Circuits
{
    
    private int num1;
    private int num2;
    private char arithOperator;
    private bool frozen;
    private bool wired;
    private int wireIndex;
    
    protected MathCircuit()
    {
        
    }
    
    //precondition: operator is supported
    //constructor
    public MathCircuit(int number1, int number2, char op)
    {
        num1 = number1;
        num2 = number2;
        if (op != '+' && op != '-' && op != '/' 
            && op != '%' && op != '*' && op != '&')
        {
            throw new ArgumentException("Invalid operator");
        }
        arithOperator = op;
        frozen = false;
    }


    private MathCircuit clone()
    {
        MathCircuit copy = new MathCircuit();
        copy.num1 = num1;
        copy.num2 = num2;
        copy.arithOperator = arithOperator;
        copy.frozen = frozen;
        copy.wired = wired;
        copy.wireIndex = wireIndex;
        return copy;
    }

    public virtual Circuits DeepCopy()
    {
       return clone();
    }

    
    public virtual int calculate()
    {
        int returnVal = 0;
        switch (arithOperator)
        {
            case '+':
                returnVal = num1 + num2;
                break;

            case '-':
                returnVal = num1 - num2;
                break;

            //precondition: num cant be zero
            case '/':
                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }
                else
                {
                    returnVal = num1 / num2;
                }
                
                break;

            case '*':
                returnVal = num1 * num2;
                break;
            
            case '&':
                returnVal = num1 & num2;
                break;
            
            //precondition: num cant be zero
            case '%':
                if (num2 == 0)
                {
                    throw new DivideByZeroException();
                }
                else
                {
                    returnVal = num1 % num2;
                }
                
                break;

        }

        return returnVal;
    }
    
    public bool isWired()
    {
        return wired;
    }
    
    //public getters to access private member values
    public int getNum1()
    {
        return num1;
    }

    public int getNum2()
    {
        return num2;
    }

    public virtual char getOperator()
    {
        return arithOperator;
    }
    
    //precondition: circuit is unfrozen
    public virtual void setOperator(char op)
    {
        if (op != '+' && op != '*' &&
            op != '-' && op != '%' &&
            op != '/' && op != '&')
        {
            throw new ArgumentException("Invalid operator!");
        }
            
            
        if (!frozen)
        {
            arithOperator = op;
        }
        else
        {
            throw new Exception("Cannot set operator to Frozen circuit");
        }
    }
    
    public void setNum1(int number1)
    {
        num1 = number1;
    }

    public void setNum2(int number2)
    {
        num2 = number2;
    } 

    //precondition: circuit is unfrozen
    //post: circuit is now frozen
    public void freeze()
    {
        if (frozen)
        {
            throw new InvalidOperationException("Circuit is already frozen!");
        }
        frozen = true;
    }
    
    public bool isFrozen()
    {
        return frozen;
    }
    
    //pre condition: circuit is frozen
    //post condition: circuit is now unfrozen
    public void unfreeze()
    {
        if (frozen)
        {
            frozen = false;
        }
        else
        {
            throw new InvalidOperationException("Circuit already unfrozen");
        }
    }
    
    //pre: circuit is unwired
    //post: circuit is now wired to a compCircuit
    public void wire(int compIndex)
    {
        if (wired == false)
        {
            wired = true;
            wireIndex = compIndex;
            
        }
        else
        {
            throw new InvalidOperationException("Circuit is already wired!");
        }
    }
    
    //Pre: circuit must be wired to correct index
    //post: circuit is now unwired
    public void unwire(int compIndex)
    {
        if (wired == true && wireIndex == compIndex)
        {
            wired = false;
            wireIndex = -1;
        }
        else if(wired == false)
        {
            throw new InvalidOperationException("Circuit is already uwired!");
        }
        else
        {
            throw new InvalidOperationException("Circuit is not wired to this Index!");
        }
    }

    public int getWireIndex()
    {
        return wireIndex;
    }
}

/* Implentation Invanriant
 *
 * Design choices:
 * - arithmetic operators are stored as chars, each operator is a single char eg. '-'
 * - isFrozen is a bool that can flags a circuit as immutable in terms of its operator.
 * - Constructor is public so that an object cam be initialized by client
 * - Deep Coopy is public which calls private clone to make a deepCopy of obj
 * - Calculate returns a int value after calculatio does not handle int division
 * - WireIndex holds index of compCircuit input Index
 * 
 *
 * bookkeeping:
 * - all data values and flags are private for encapsulation,
 * they cannot be directly accessed by client
 */ 