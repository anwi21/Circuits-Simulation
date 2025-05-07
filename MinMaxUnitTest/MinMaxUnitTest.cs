//Anwi Gundavarapu
//CPSC3200: P3 Unit Test
//5/4/2025
//Version 1

namespace MinMaxUnitTest;
using CircuitsInterface;
using MinMaxCircuits;
using MathCircuits;

[TestClass]
public class MinMaxUnitTest
{
    [TestMethod]
    public void TestConstructorGettersSetters()
    {
        Circuits mc = new MinMaxCircuit(1, 1, '<');
        Assert.AreEqual(1, mc.getNum1());
        Assert.AreEqual(1, mc.getNum2());
        Assert.AreEqual('<', mc.getOperator());
        mc.setNum2(2);
        Assert.AreEqual(2, mc.getNum2());
        Assert.ThrowsException<ArgumentException>(() => new MinMaxCircuit(1, 1, '!'));
        Assert.AreEqual(1, mc.calculate());
        mc.setOperator('>');
        Assert.AreEqual(2, mc.calculate());
        mc.freeze();
        mc.wire(3); //for testing purposes
        Circuits mc2 = mc.DeepCopy();
        Assert.AreEqual(mc.getNum1(), mc2.getNum1());
        mc2.setNum1(3);
        Assert.AreNotEqual(mc.getNum1(), mc2.getNum1());
        Assert.ThrowsException<ArgumentException>(() => mc2.setOperator('/'));
    }
    
}