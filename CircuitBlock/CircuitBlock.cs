//Anwi Gundavarapu
//CPSC3200: CircuitBlock P3
//5/4/2025
//Version 2

using CircuitsInterface;
using MathCircuits;
using ComparisonCircuits;
using MinMaxCircuits;


namespace CircuitBlocks;


/*
 * Class: CircuitBlock Class
 * -----------------------------------------------------------------
 * Expected State and Conditions:
 * -  * - A client can access all the public functionality of the standalone circuits in the Circuist Interface
 * - Once a circuitBlock is frozen, it can be unfrozen using unfreeze().
 * - When constructed, a client must initialize L, M, N minMax, math and comparison circuits respectively.
 * - If an unsupported operator is used, an exception is thrown
 * - The object starts in an unfrozen state, allowing changes to the operator.
 * - Changing the state when a circuit is frozen will result in an exception thrown.
 *   when an illegal operation is attempted, an exception is thrown
 * - The class always returns a valid value as long as the state is supported.
 * - An instance of this circuit can be deep copied to another using DeepCopy
 * - A client can wire circuits within this Block using wire and unwire()
 * -  A client can access members of the standalone circuits in this block using getters and change their values
 * setters
 * - A client can view the state of each circuit as well as the block using isWired or isFrozen.
 * - ------------------------------------------------------------------------
 * 
 * Relationship between mutators:
 * - freeze() disables setOperator()
 *
 */
public class CircuitBlock
{
    
    private int M, N, L; //L;
    private int[] mathWired;
    private int[] minMaxWired;
    private Circuits[] minMaxCircuits;
    private Circuits[] mathCircuits;
    private Circuits[] comparisonCircuits;
    private bool frozen;
    //private CircuitBlock refd;

    private CircuitBlock()
    {
       
    }
    private CircuitBlock clone()
    {
        CircuitBlock copy = new CircuitBlock();
        copy.M = M;
        copy.N = N;
        copy.L = L;
        
        copy.mathWired = new int[copy.M];
        copy.minMaxWired = new int[L];

        copy.minMaxCircuits = new Circuits[L];
        for (int i = 0; i < L; i++)
        {
            copy.minMaxWired[i] = minMaxWired[i];
            copy.minMaxCircuits[i] = minMaxCircuits[i].DeepCopy();
        }
        
        copy.mathCircuits = new Circuits[M];
        for (int i = 0; i < M; i++)
        {
            copy.mathWired[i] = mathWired[i];
            copy.mathCircuits[i] = mathCircuits[i].DeepCopy();
        }
        copy.comparisonCircuits = new Circuits[N];
        for (int i = 0; i < N; i++)
        {
            copy.comparisonCircuits[i] = comparisonCircuits[i].DeepCopy();
        }
        
        copy.frozen = frozen;
        
        return copy;

    }

    public CircuitBlock DeepCopy()
    {
        return clone();
    }
    
    public CircuitBlock(int l, int m, int n, char[] minMaxOps, char[] mathOps, char[] compOps)
    {
        M = m;
        N = n;
        L = l;
        
        
        mathWired = new int[M];
        for (int i = 0; i < M; i++)
        {
            mathWired[i] = -1;
        }
        
        minMaxWired = new int[L];
        for (int i = 0; i < L; i++)
        {
            minMaxWired[i] = -1;
        }
        
        minMaxCircuits = new Circuits[L];
        for (int i = 0; i < L; i++)
        {
            minMaxCircuits[i] = new MinMaxCircuit(0, 0, minMaxOps[i]);
        } 
        
        mathCircuits = new Circuits[M];
        for (int i = 0; i < M; i++)
        {
            mathCircuits[i] = new MathCircuit(0, 0, mathOps[i]);
        } 
        
        comparisonCircuits = new Circuits[N];
        for (int i = 0; i < N; i++)
        {
            comparisonCircuits[i] = new ComparisonCircuit(0, 0, compOps[i]);
        }
        
    }
    
    public void setMinMaxNum1(int minMaxCircuitIndex, int num)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax circuit index");
        }
        minMaxCircuits[minMaxCircuitIndex].setNum1(num);
    }

    public Circuits minMaxDeepCopy(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax Circuit index");
        }
        return minMaxCircuits[minMaxCircuitIndex].DeepCopy();
    }
    
    public void setMinMaxNum2(int minMaxCircuitIndex, int num)
    {
        if(minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax circuit index");
        }
        minMaxCircuits[minMaxCircuitIndex].setNum2(num);
    }

    public int getMinMaxNum1(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax circuit index");
        }
        return minMaxCircuits[minMaxCircuitIndex].getNum1();
    }

    public int getMinMaxNum2(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax circuit index");
        }
        return minMaxCircuits[minMaxCircuitIndex].getNum2();
    }

    public void setMinMaxState(int minMaxCircuitIndex, char op)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax circuit index");
        }

        if (frozen)
        {
            throw new InvalidOperationException("Circuit block is frozen!");
        }

        if (minMaxCircuits[minMaxCircuitIndex].isFrozen())
        {
            throw new InvalidOperationException("MinMaxCircuit is frozen!");
        }

        minMaxCircuits[minMaxCircuitIndex].setOperator(op);
    }

    public char getMinMaxState(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMac circuit index");
        }
        return minMaxCircuits[minMaxCircuitIndex].getOperator();
    }

    //pre: circuit unfrozen
    //post: circuit frozne
    public void minMaxFreeze(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid MinMax Circuit index");
        }
        minMaxCircuits[minMaxCircuitIndex].freeze();
    }

    //pre: circuit frozen
    //post: circuit unfrozne
    public void minMaxUnfreeze(int minMaxCircuitIndex)
    {
        if(minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        minMaxCircuits[minMaxCircuitIndex].unfreeze();
    }

    public bool isMinMaxFrozen(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return minMaxCircuits[minMaxCircuitIndex].isFrozen();
    }
    
    public int getMinMaxResult(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        return minMaxCircuits[minMaxCircuitIndex].calculate();
    }
    
    public Circuits mathDeepCopy(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return mathCircuits[mathCircuitIndex].DeepCopy();
    }
    
    public void setMathNum1(int mathCircuitIndex, int num)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        mathCircuits[mathCircuitIndex].setNum1(num);
    }
    
    public void setMathNum2(int mathCircuitIndex, int num)
    {
        if(mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        mathCircuits[mathCircuitIndex].setNum2(num);
    }

    public int getMathNum1(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return mathCircuits[mathCircuitIndex].getNum1();
    }

    public int getMathNum2(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return mathCircuits[mathCircuitIndex].getNum2();
    }
    
    public void setMathOperator(int mathCircuitIndex, char op)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        if (frozen)
        {
            throw new InvalidOperationException("Circuit block is frozen!");
        }

        if (mathCircuits[mathCircuitIndex].isFrozen())
        {
            throw new InvalidOperationException("Math Circuit is frozen!");
        }
        mathCircuits[mathCircuitIndex].setOperator(op);
    }

    public char getMathOperator(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return mathCircuits[mathCircuitIndex].getOperator();
    }

    //pre: circuit unfrozen
    //post: circuit frozne
    public void mathFreeze(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        mathCircuits[mathCircuitIndex].freeze();
    }

    //pre: circuit frozen
    //post: circuit unfrozne
    public void mathUnfreeze(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        mathCircuits[mathCircuitIndex].unfreeze();
    }

    
    public bool isMathFrozen(int mathCircuitIndex)
    {
        return mathCircuits[mathCircuitIndex].isFrozen();
    }
    
    public int getMathResult(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        return mathCircuits[mathCircuitIndex].calculate();
    }
    
    public Circuits compDeepCopy(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return comparisonCircuits[compCircuitIndex].DeepCopy();
    }

    
    public void setCompNum1(int compCircuitIndex, int num)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        comparisonCircuits[compCircuitIndex].setNum1(num);
    }

    public void setCompNum2(int compCircuitIndex, int num)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        comparisonCircuits[compCircuitIndex].setNum2(num);
    }

    public int getCompNum1(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return comparisonCircuits[compCircuitIndex].getNum1();
    }

    public int getCompNum2(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return comparisonCircuits[compCircuitIndex].getNum2();
    }
    
    public void setCompOperator(int compCircuitIndex, char op)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        else if (frozen)
        {
            throw new InvalidOperationException("Circuit block is frozen!");
        }
        
        else if (comparisonCircuits[compCircuitIndex].isFrozen())
        {
            throw new InvalidOperationException("Comparison Circuit is frozen!");
        }
        else
        {
            comparisonCircuits[compCircuitIndex].setOperator(op);
        }
    }

    public char getCompOperator(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return comparisonCircuits[compCircuitIndex].getOperator();
    }

    public int getCompResult(int compCircuitIndex)
    {
        return comparisonCircuits[compCircuitIndex].calculate();
    }
    
    
    //pre: circuit unfrozen
    //post: circuit frozne
    public void compFreeze(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        comparisonCircuits[compCircuitIndex].freeze();
    }

    //pre: circuit frozen
    //post: circuit unfrozen
    public void compUnfreeze(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        comparisonCircuits[compCircuitIndex].unfreeze();
    }

    public bool isCompFrozen(int compCircuitIndex)
    {
        if (compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }
        return comparisonCircuits[compCircuitIndex].isFrozen();
    }



    //pre: circuit unwired
    //post: circuit wired
    public void mathWire(int mathCircuitIndex, int compCircuitIndex, int inputIndex)
    {
        if ((mathCircuitIndex < 0 || mathCircuitIndex >= M)
            || compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit indexes");
        }

        if (mathCircuits[mathCircuitIndex].isWired())
        {
            throw new InvalidOperationException("Math Circuit is already wired");
        }

        if (inputIndex == 1)
        {
            comparisonCircuits[compCircuitIndex].
                setNum1(mathCircuits[mathCircuitIndex].
                    calculate());
            mathWired[mathCircuitIndex] = compCircuitIndex;
            comparisonCircuits[compCircuitIndex].wire(inputIndex);
            mathCircuits[mathCircuitIndex].wire(inputIndex);
        }
        
        else if (inputIndex == 2)
        {
            comparisonCircuits[compCircuitIndex].
                setNum2(mathCircuits[mathCircuitIndex].
                    calculate()); 
            mathWired[mathCircuitIndex] = compCircuitIndex;
            comparisonCircuits[compCircuitIndex].wire(inputIndex);
            mathCircuits[mathCircuitIndex].wire(inputIndex);
        }
        

        else
        {
            throw new IndexOutOfRangeException
                ("Invalid comparison circuit input index");
        }
            
    }
    

    //pre: circuit wired
    //post: circuit unwired
    public void mathUnwire(int mathCircuitIndex)
    {
        if (mathCircuitIndex < 0 || mathCircuitIndex >= M)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        if (mathWired[mathCircuitIndex] == -1)
        {
            throw new ArgumentException("MathCircuit is not wired to any Comparison Circuits!");
        }

        int compIndex = mathWired[mathCircuitIndex];
        int inputIndex = mathCircuits[mathCircuitIndex].getWireIndex();
        
        mathWired[mathCircuitIndex] = -1;
        comparisonCircuits[compIndex].unwire(inputIndex);
        
    }
    
    //pre: circuit unwired
    //post: circuit wired
    public void minMaxWire(int minMaxCircuitIndex, int compCircuitIndex, int inputIndex)
    {
        if ((minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L)
            || compCircuitIndex < 0 || compCircuitIndex >= N)
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        if (mathCircuits[minMaxCircuitIndex].isWired())
        {
            throw new ArgumentException("MinMax Circuit is already wired");
        }

        if (inputIndex == 1)
        {
            comparisonCircuits[compCircuitIndex].
                setNum1(minMaxCircuits[minMaxCircuitIndex].
                    calculate());
            minMaxWired[minMaxCircuitIndex] = compCircuitIndex;
            comparisonCircuits[compCircuitIndex].wire(inputIndex);
            minMaxCircuits[minMaxCircuitIndex].wire(inputIndex);
        }
        
        else if (inputIndex == 2)
        {
            comparisonCircuits[compCircuitIndex].
                setNum2(minMaxCircuits[minMaxCircuitIndex].
                    calculate()); 
            minMaxWired[minMaxCircuitIndex] = compCircuitIndex;
            comparisonCircuits[compCircuitIndex].wire(inputIndex);
            minMaxCircuits[minMaxCircuitIndex].wire(inputIndex);
        }
        

        else
        {
            throw new IndexOutOfRangeException
                ("Invalid comparison circuit input index");
        }
            
    }
    
    //pre: circuit wired
    //post: circuit unwired
    public void minMaxUnwire(int minMaxCircuitIndex)
    {
        if (minMaxCircuitIndex < 0 || minMaxCircuitIndex >= L )
        {
            throw new IndexOutOfRangeException("Invalid circuit index");
        }

        if (minMaxWired[minMaxCircuitIndex] == -1)
        {
            throw new ArgumentException("MinMaxCircuit is not wired to any Comparison Circuits!");
        }

        int compIndex = minMaxWired[minMaxCircuitIndex];
        int inputIndex = minMaxCircuits[minMaxCircuitIndex].getWireIndex();
        minMaxWired[minMaxCircuitIndex] = -1;
        comparisonCircuits[compIndex].unwire(inputIndex);
        
        
    }

    
    //pre:block is unfrozen
    //post: block is frozen
    public void freeze()
    {
        if (!frozen)
        {
            frozen = true;

            for (int i = 0; i < L; i++)
            {
                if (!minMaxCircuits[i].isFrozen())
                {
                    minMaxCircuits[i].freeze();
                }
            }
            for (int i = 0; i < M; i++)
            {
                if (!mathCircuits[i].isFrozen())
                {
                    mathCircuits[i].freeze();
                }
            }

            for (int i = 0; i < N; i++)
            {
                if (!comparisonCircuits[i].isFrozen())
                {
                    comparisonCircuits[i].freeze();
                }
            }
        }
        else
        {
            throw new InvalidOperationException("CircuitBlock is already frozen");
        }
    }

    //pre:block is frozen
    //post: block is unfrozen
    public void unfreeze()
    {
        if (frozen == true)
        {
            frozen = false;
            for (int i = 0; i < L; i++)
            {
                if (minMaxCircuits[i].isFrozen())
                {
                    minMaxCircuits[i].unfreeze();
                }
            }
            
            for (int i = 0; i < M; i++)
            {
                if (mathCircuits[i].isFrozen())
                {
                  mathCircuits[i].unfreeze();  
                }
            }

            for (int i = 0; i < N; i++)
            {
                if (comparisonCircuits[i].isFrozen())
                {
                    comparisonCircuits[i].unfreeze();
                }
            }
        }
        else
        {
            throw new InvalidOperationException("CircuitBlock is already unfrozen");
        }
    }
    
    public bool isFrozen()
    {
        return frozen;
    }
}


/*Class Invariant
 *
 * This class is designed to hold L, M and N minmax, math and comparison circuits respectively.
 * These circuits are held in 3 Circuit containers of respective sizes.
 * Deep Copy is a public method that calls a private clone method.
 * This block allows wiring between circuits.
 * Only a comparison circuit can have its inputs wired.
 * mathWire and minMaxWire take in a comparison circuit index and its input index,
 * these indicate which comparison circuit their output would be wired to, and which input of that circuit.
 * Exceptions are used to ensure that wiring is valid.
 * Unwiring takes in the mathCircuit/ minmaxCircuit index which then unwires the corresponding circuits output
 * to a comparison circuit
 * Wiring and Unwiring a a circuit calls on its corresponding wiring method.
 * heling a circuit keep track of its wiring state.
 * */