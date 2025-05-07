//Anwi Gundavarapu
//CPSC3200: Comparison Circuit Class
//5/4/2025
//Version 3

using CircuitsInterface;
namespace ComparisonCircuits;

/*
 * Class: Comparison Circuit
 * -----------------------------------------------------------------
 * Expected State and Conditions:
 * - Once a circuit is frozen, it can be unfrozen using unfreeze().
 * - When constructed, the circuit holds two integers and a supported operator.
 * - Supported operators are: '>','<',','!','='
 * - If an unsupported operator is used, an exception is thrown
 * - The object starts in an unfrozen state, allowing changes to the operator.
 * - Once frozen, the operator cannot be changed.
 * - Changing the operator when a circuit is frozen will result in an exception thrown.
 *   when an illegal operation is attempted, an exception is thrown
 * - The class always returns a valid value as long as the operator is a supported operator
 * - An instance of this circuit can be deep copied to another using DeepCopy
 * - This Circuit allows wiring, where this circuit's output can be wired to a comparison circuit
 * - The circuit must be unwired before wiring and vice versa.
 * - Once wired, a client cannot change the value of the wiredIndex unless unwired.
 * - Any invalid state changes result in an exception
 * - A client may use getWiredIndex() to get the inputIndex of its result in its wired CompCircuit.
 *
 * ------------------------------------------------------------------------
 * Relationship between mutators:
 * - freeze() disables setOperator()
 * - wire() disables setNum1() or setNum2() based on wiring
 *
 */
public class ComparisonCircuit : Circuits
{      
   //members
   private int num1;
   private int num2;
   private char compOperator;
   private bool frozen;
   private int[] wiring;

   private ComparisonCircuit()
   {
       
   }
   private ComparisonCircuit clone()
   {
       ComparisonCircuit copy = new ComparisonCircuit();
       copy.num1 = num1;
       copy.num2 = num2;
       copy.frozen = frozen;
       copy.compOperator = compOperator;
       copy.wiring = new int[2] { 0, 0 };
       for (int i = 0; i < 2; i++)
       {
           copy.wiring[i] = wiring[i];
       }
       return copy;
   }
   
   public Circuits DeepCopy()
   {
       return clone();
   }
   
   //precondition: operator is supported
   public ComparisonCircuit(int number1, int number2, char op)
   {
       num1 = number1;
       num2 = number2;
       if (op != '=' && op != '!' && op != '>' && op != '<')
       {
           throw new ArgumentException("Invalid operator");
       }
       compOperator = op;
       frozen = false;
       wiring = new int[2];
       for (int i = 0; i < 2; i++)
       {
           wiring[i] = 0;
       }
   }
   
   //preconiditon: operator is valid
   public int calculate()
   {
       int returnVal = 0;
       switch (compOperator)
       {
           case '>':
               if (num1 > num2)
               {
                   returnVal = 1;
               }
               else
               {
                   returnVal = 0;
               }
               break;
            
           case '<':
               if (num1 < num2)
               {
                   returnVal = 1;
               }
               else
               {
                   returnVal = 0;
               }
               break;
            
           case '!':
               if (num1 != num2)
               {
                   returnVal = 1;
               }
               else
               {
                   returnVal = 0;
               }
               break;
           
           case '=':
               if (num1 == num2)
               {
                   returnVal = 1;
               }
               else
               {
                   returnVal = 0;
               }
               
               break;
            
       }
       return returnVal;
   }
   
    public int getNum1()
    {
        return num1;
    }

    public int getNum2()
    {
        return num2;
    }

    public char getOperator()
    {
        return compOperator;
    }
    
    //public number setter methods
    public void setNum1(int number1)
    {
        if (wiring[0] != 0)
        {
            throw new ArgumentException("Cannot Set Number, input is wired to a mathCiruit");
        }
        num1 = number1;
    }

    public void setNum2(int number2)
    {
        if (wiring[1] != 0)
        {
            throw new ArgumentException("Cannot Set Number, input is wired to a mathCiruit");
        }
        num2 = number2;
    }
    
    //precondition: circuit is unfrozen?
    public void setOperator(char op)
    {
        if (op == '>' || op == '<' || op == '!' || op == '=')
        {
            if (!frozen)
            {
                compOperator = op;
            }
            else
            {
                throw new ArgumentException("Cannot set operator because it is frozen.");
            } 
        }
        else
        {
            throw new ArgumentException("Invalid operator.");
        }
    }
    
    //precondition: circuit is unfrozen
    //postcondition: circuit is frozen
    public void freeze()
    {
        frozen = true;
    }
   
    //pre: circuit is frozen
    //post: circuit is unfrozen
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


    public bool isFrozen()
    {
        return frozen;
    }

    //pre: circuit is unwired
    //post: corcuits is wired
    public void wire(int wireIndex)
    {

        if (wireIndex > 2 || wireIndex < 1)
        {
            throw new IndexOutOfRangeException("Invalid wire index. Index must be 1 or 2");
        }

        if (wiring[wireIndex - 1] != 0 )
        {
            throw new ArgumentException("CompCircuit already wired at this input.");
        }
        
        
        
        if (wireIndex == 1)
        {
            wiring[0] = 1;
                
        }

        if (wireIndex == 2)
        {
            wiring[1] = 1;
        }
        
        
    }
    
    //pre: circuit is wired
    //post: circuit is unwired
    public void unwire(int inputIndex)
    {
        if (inputIndex > 2 || inputIndex < 1)
        {
            throw new IndexOutOfRangeException("Invalid wire index. Index must be 1 or 2");
        }
        
        if (wiring[inputIndex-1] == 1)
        {
            wiring[inputIndex-1] =  0;
        }
        
        else
        {
            throw new ArgumentException("MathCircuit is not wired to this Comparison Circuit!");
        }
        
    }

    public int getWireIndex()
    {
        if (wiring[0] == 0 && wiring[1] == 0)
        {
            throw new InvalidOperationException("Circuit is not wired");
        }

        if (wiring[0] == 1 && wiring[1] == 0)
        {
            return 1;
        }

        if (wiring[0] == 0 && wiring[1] == 1)
        {
            return 2;
        }

        throw new ArgumentException("CompCircuit is not uniquely wired, check connected Circuits!");

    }
    
    public bool isWired()
    {
        return (wiring[0] != 0) && (wiring[1] != 0);
    }

    
}

/*
 *
 * Implementation Invariant
 * design choices:
 * - comparison operators are stored as har, so that we can have comparisons like >
 * - isFrozen is a bool that can flags a circuit as immutable in terms of its operator.
 * - Once a circuit is frozen, it cannot be unfrozen.
 * - Constructor is public so that an object cam be initialized by client
 * - Deep Coopy is public which calls private clone to make a deepCopy of obj
 * - Calculate returns an int value 1 or 0 based on if comparaison is true or false.
 * - wiring
 *
 * bookkeeping:
 * - all parameters are call-by-value.
 * - all data values and flags are private for encapsulation, they cannot be directly accessed by client
 */ 