//Anwi Gundavarapu
//CPSC3200: P3 Unit Test
//5/4/2025
//Version 1

using CircuitsInterface;

namespace CircuitBlockTest;
using CircuitBlocks;

[TestClass]
public class UnitTest1
{
     [TestMethod]
    public void TestCircuitBlockOperations()
    {
        int L = 2, M = 2, N = 2;
        char[] LOps = { '>', '<' };
        char[] MOps = { '-', '+' };
        char[] NOps = { '!', '=' };
        CircuitBlock cb = new CircuitBlock(L,M,N,LOps,MOps,NOps);
        cb.setCompNum1(0,1);
        cb.setCompNum2(0,2);
        cb.setCompOperator(0, '>');
        CircuitBlock copy = cb.DeepCopy();
        Assert.AreEqual(cb.getCompResult(0), copy.getCompResult(0));
        copy.setCompNum1(0, 3);
        copy.setCompNum2(0, 1);
        Assert.AreNotEqual(cb.getCompResult(0),copy.getCompResult(0));
        Assert.AreEqual(false, copy.isFrozen());
        cb.freeze();
        Assert.ThrowsException<InvalidOperationException>(() => cb.freeze());
        Assert.AreEqual(true, cb.isFrozen());
        cb.unfreeze();
        Assert.ThrowsException<InvalidOperationException>(() => cb.unfreeze());
    }

    [TestMethod]
    public void TestSettersGetters()
    {
        int L = 2, M = 2, N = 2;
        char[] LOps = { '<', '<' };
        char[] MOps = { '-', '-' };
        char[] NOps = { '>', '>' };
       CircuitBlock cb = new CircuitBlock(L,M,N,LOps,MOps,NOps);
       cb.setCompNum1(0,1);
       cb.setCompNum2(0,2);
       Assert.AreEqual(0, cb.getCompResult(0));
       cb.setMathNum1(1,3);
       cb.setMathNum2(1,2);
       cb.setMathOperator(1, '+');
       Assert.AreEqual(5, cb.getMathResult(1));
       cb.setMinMaxNum1(0,4);
       cb.setMinMaxNum2(0,5);
       cb.setMinMaxState(0,'<');
       Assert.AreEqual(4, cb.getMinMaxResult(0));
       Assert.AreEqual(1, cb.getCompNum1(0));
       Assert.AreEqual(2, cb.getCompNum2(0));
       Assert.AreEqual('>', cb.getCompOperator(1));
       Assert.AreEqual(3, cb.getMathNum1(1));
       Assert.AreEqual(2, cb.getMathNum2(1));
       Assert.AreEqual('-', cb.getMathOperator(0));
       Assert.AreEqual(4, cb.getMinMaxNum1(0));
       Assert.AreEqual(5, cb.getMinMaxNum2(0));
       Assert.AreEqual('<', cb.getMinMaxState(1));
    }

    [TestMethod]
    public void TestMinMaxExceptions()
    {
        int L = 1, M = 2, N = 1;
        char[] LState = { '>' };
        char[] MOps = { '%','/' };
        char[] NOps = { '!' };
        CircuitBlock cb = new CircuitBlock(L,M,N,LState,MOps,NOps);
        Assert.ThrowsException<ArgumentException>(() => cb.setMinMaxState(0,'!'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMinMaxResult(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMinMaxNum1(1,1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMinMaxNum2(1,2));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMinMaxNum1(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMinMaxNum2(1));
        Assert.ThrowsException<InvalidOperationException>(() => cb.minMaxUnfreeze(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxFreeze(1));
        cb.minMaxFreeze(0);
        Assert.AreEqual(true, cb.isMinMaxFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxFreeze(1));
        cb.minMaxUnfreeze(0);
        Assert.AreEqual(false, cb.isMinMaxFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMinMaxState(1,'<'));
        Circuits x = cb.minMaxDeepCopy(0);
        cb.minMaxFreeze(0);
        Assert.ThrowsException<InvalidOperationException>(() => cb.setMinMaxState(0,'>'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxDeepCopy(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMinMaxState(2,'!'));
        cb.freeze();
        Assert.ThrowsException<InvalidOperationException>(() => cb.setMinMaxState(0,'<'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.isMinMaxFrozen(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxUnfreeze(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMinMaxState(1));
    }

    [TestMethod]
    public void TestMathOperations()
    {
        int L = 1, M = 1, N = 1;
        char[] LState = { '>' };
        char[] MOps = { '%' };
        char[] NOps = { '!' };
        CircuitBlock cb = new CircuitBlock(L,M,N,LState,MOps,NOps); 
        Assert.ThrowsException<ArgumentException>(() => cb.setMathOperator(0,'!'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMathResult(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMathNum1(1,1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMathNum2(1,2));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMathNum1(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMathNum2(1));
        Assert.ThrowsException<InvalidOperationException>(() => cb.mathUnfreeze(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathFreeze(1));
        cb.mathFreeze(0);
        Assert.AreEqual(true, cb.isMathFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathFreeze(1));
        cb.mathUnfreeze(0);
        Assert.AreEqual(false, cb.isMathFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMathOperator(1,'<'));
        cb.mathFreeze(0);
        Circuits x = cb.mathDeepCopy(0);
        Assert.AreEqual(cb.isMathFrozen(0), x.isFrozen());
        x.unfreeze();
        Assert.AreNotEqual(cb.isMathFrozen(0), x.isFrozen());
        Assert.ThrowsException<InvalidOperationException>(() => cb.setMathOperator(0,'!'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathDeepCopy(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setMathOperator(2,'+'));
        cb.freeze();
        Assert.ThrowsException<InvalidOperationException>(() => cb.setMathOperator(0,'+'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.isMathFrozen(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathUnfreeze(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getMathOperator(1));
    }
    
    [TestMethod]
    public void TestCompOperations()
    {
        int L = 5, M = 2, N = 1;
        char[] LState = { '>','>','>','>','>'};
        char[] MOps = { '%','/' };
        char[] NOps = { '!'};
        CircuitBlock cb = new CircuitBlock(L,M,N,LState,MOps,NOps); 
        Assert.ThrowsException<ArgumentException>(() => cb.setCompOperator(0,':'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getCompResult(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setCompNum1(1,1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setCompNum2(1,2));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getCompNum1(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getCompNum2(1));
        Assert.ThrowsException<InvalidOperationException>(() => cb.compUnfreeze(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.compFreeze(1));
        cb.compFreeze(0);
        Assert.AreEqual(true, cb.isCompFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.compFreeze(1));
        cb.compUnfreeze(0);
        Assert.AreEqual(false, cb.isCompFrozen(0));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setCompOperator(1,'<'));
        cb.compFreeze(0);
        Circuits x = cb.compDeepCopy(0);
        Assert.AreEqual(cb.isCompFrozen(0), x.isFrozen());
        x.unfreeze();
        Assert.AreNotEqual(cb.isCompFrozen(0), x.isFrozen());
        Assert.ThrowsException<InvalidOperationException>(() => cb.setCompOperator(0,'!'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.compDeepCopy(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.setCompOperator(2,'+'));
        cb.freeze();
        Assert.ThrowsException<InvalidOperationException>(() => cb.setCompOperator(0,'+'));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.isCompFrozen(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.compUnfreeze(1));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.getCompOperator(1));
        
    }
    
    [TestMethod]
    public void TestMathWiring()
    {
        int L = 1, M = 2, N = 1;
        char[] LState = { '>' };
        char[] MOps = { '%','/' };
        char[] NOps = { '!' };
        CircuitBlock cb = new CircuitBlock(L,M,N,LState,MOps,NOps);
        cb.setMathOperator(0,'+');
        cb.setMathNum1(0,1);
        cb.setMathNum2(0,2);
        cb.mathWire(0,0,1);
        Assert.AreEqual(cb.getCompNum1(0), cb.getMathResult(0));
        Assert.ThrowsException<IndexOutOfRangeException>(()=>cb.mathWire(0,1,1));
        Assert.ThrowsException<IndexOutOfRangeException>(()=>cb.mathWire(1,1,2));
        Assert.ThrowsException<InvalidOperationException>(()=>cb.mathWire(0, 0,2));
        cb.mathUnwire(0);
        Assert.ThrowsException<InvalidOperationException>(()=>cb.mathWire(0,0,2));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathWire(1, 0, 3)); 
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathWire(-1, 0, 1)); 
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.mathWire(M, 0, 1));
        cb.setMathNum1(1, 1);
        cb.setMathNum2(1, 2);
        Assert.ThrowsException<ArgumentException>(() => cb.mathUnwire(1));


    }
    
    [TestMethod]

    public void TestMinMaxWiring()
    {
        int L = 2, M = 2, N = 1;
        char[] LState = { '>','>' };
        char[] MOps = { '%','/' };
        char[] NOps = { '!' };
        CircuitBlock cb = new CircuitBlock(L,M,N,LState,MOps,NOps);
        cb.setMinMaxState(0,'>');
        cb.setMinMaxNum1(0,1);
        cb.setMinMaxNum2(0,2);
        cb.minMaxWire(0,0,1);
        Assert.AreEqual(cb.getCompNum1(0), cb.getMinMaxResult(0));
        Assert.ThrowsException<IndexOutOfRangeException>(()=>cb.minMaxWire(0,1,1));
        Assert.ThrowsException<IndexOutOfRangeException>(()=>cb.minMaxWire(1,1,2));
        Assert.ThrowsException<InvalidOperationException>(()=>cb.minMaxWire(0, 0,2));
        cb.minMaxUnwire(0);
        Assert.ThrowsException<ArgumentException>(()=>cb.minMaxWire(0,0,2));
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxWire(1, 0, 3)); 
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxWire(-1, 0, 1)); 
        Assert.ThrowsException<IndexOutOfRangeException>(() => cb.minMaxWire(M, 0, 1));
        cb.setMinMaxNum1(1, 1);
        cb.setMinMaxNum2(1, 2);
        Assert.ThrowsException<ArgumentException>(() => cb.minMaxUnwire(1));
    }
    



}