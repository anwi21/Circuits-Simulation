//Anwi Gundavarapu
//CPSC 3200: P3 Driver
//5/4/2025
//Version 1


namespace PA3;
using CircuitBlocks;
using CircuitsInterface;
using ComparisonCircuits;
using MathCircuits;
using MinMaxCircuits;



class P3
{
    static void Main(string[] args)
    {
        Circuits[] circuits = new Circuits[4];
        circuits[0] = getMathCircuit();
        circuits[1] = getMinMaxCircuit();
        circuits[2] = getComparisonCircuit();
        circuits[3] = circuits[0].DeepCopy();
        
        useCircuitInterface(circuits);

        CircuitBlock cb = getCircuitBlock();
        TestCircuitBlockValues(cb);
        CircuitBlockFreezing(cb);
        CircuitBlockWiring(cb);

        exceptionDriver();
    }

    static MathCircuit getMathCircuit()
    {
        MathCircuit mc = new MathCircuit(5, 2, '+');
        mc.setNum1(10);
        mc.setNum2(4);
        mc.setOperator('*');
        return mc;
    }

    static ComparisonCircuit getComparisonCircuit()
    {
        ComparisonCircuit cc = new ComparisonCircuit(4, 4, '=');
        cc.setOperator('!');
        cc.setNum1(6);
        cc.setNum2(7);
        return cc;
    }

    static MinMaxCircuit getMinMaxCircuit()
    {
        MinMaxCircuit mm = new MinMaxCircuit(2, 9, '<');
        mm.setNum1(3);
        mm.setNum2(8);
        mm.setOperator('>');
        return mm;
    }

    static CircuitBlock getCircuitBlock()
    {
        char[] minMaxOps = { '<' };
        char[] mathOps = { '+' };
        char[] compOps = { '>' };

        CircuitBlock cb = new CircuitBlock(1, 1, 1, minMaxOps, mathOps, compOps);
        cb.setMathNum1(0, 1);
        cb.setMathNum2(0, 2);
        cb.setMinMaxNum1(0, 7);
        cb.setMinMaxNum2(0, 3);
        cb.setCompOperator(0, '>');

        return cb;
    }
    

    static void useCircuitInterface(Circuits[] circuits)
    {
        for (int i = 0; i < circuits.Length; i++)
        {
            Console.WriteLine("Circuit " + i + " result: " + circuits[i].calculate());

            circuits[i].freeze();
            Console.WriteLine("Circuit " + i + " frozen: " + circuits[i].isFrozen());

            Circuits copy = circuits[i].DeepCopy();
            Console.WriteLine("Deep Copy:" + i +" "+ copy.calculate());
            Console.WriteLine("OGCircuit Befoer Change: " + i + " " + circuits[i].getNum1());
            Console.WriteLine("CopyCircuit Before Change: " + i + " " + copy.getNum1());
            copy.setNum1(i+10);
            Console.WriteLine("OGCircuit After Change: " + i + " " + circuits[i].getNum1());
            Console.WriteLine("CopyCircuit After Change: " + i + " " + copy.getNum1());
        }
    }

    static void TestCircuitBlockValues(CircuitBlock cb)
    {
        Console.WriteLine("Math result: " + cb.getMathResult(0));
        Console.WriteLine("MinMax result: " + cb.getMinMaxResult(0));
        Console.WriteLine("Comp result: " + cb.getCompResult(0));
    }

    static void CircuitBlockFreezing(CircuitBlock cb)
    {

        cb.mathFreeze(0);
        cb.minMaxFreeze(0);
        cb.compFreeze(0);
        Console.WriteLine("Math frozen: " + cb.isMathFrozen(0));
        Console.WriteLine("MinMax frozen: " + cb.isMinMaxFrozen(0));
        Console.WriteLine("Comp frozen: " + cb.isCompFrozen(0));

        cb.freeze();
        Console.WriteLine("Block frozen: " + cb.isFrozen());

        cb.unfreeze();
        Console.WriteLine("Block unfrozen: " + (!cb.isFrozen()));
    }
    
    static void CircuitBlockWiring(CircuitBlock cb)
    {
        Console.WriteLine("Before Wiring: Comp input1 = " + cb.getCompNum1(0));

        
        
        cb.mathWire(0, 0, 1);

        Console.WriteLine("After Wiring: Comp input1 = " + cb.getCompNum1(0));

        cb.mathUnwire(0);
        Console.WriteLine("CircuitUnired");
    }
    

    static void exceptionDriver()
    {
        
        try
        {
            MinMaxCircuit bad = new MinMaxCircuit(1, 2, '!');
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
        
        try
        {
            MathCircuit mc = new MathCircuit(4, 2, '+');
            mc.setOperator('?');
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Error " + e.Message);
        }
        
        try
        {
            CircuitBlock cb = getCircuitBlock();
            cb.freeze();
            cb.freeze();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Error:" + e.Message);
        }
        
        try
        {
            CircuitBlock cb = getCircuitBlock();
            cb.getMathResult(3);
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("Errror: " + e.Message);
        }
        
        try
        {
            CircuitBlock cb = getCircuitBlock();
            cb.mathWire(0, 0, 1);
            cb.mathWire(0, 0, 2);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}