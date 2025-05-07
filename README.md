CPSC 3200 P3: Circuit Simulation

This project simulates logic circuits using object-oriented programming in C#. It includes three types of circuits: MathCircuit, ComparisonCircuit, and MinMaxCircuit, all implementing a common Circuits interface. These circuits are managed by a CircuitBlock class, allowing dynamic wiring and freezing operations. This project allows a client to manage these circuits as standalones, while also allowing a client to manage these circuits through the CircuitBlock.

----------------------------------------------------------------------------
Standalone Circuits: take in 2 inputs and evaluate to a single value.
MathCircuit: Performs arithmetic operations: '+','-','/','%','*','&'
ComparisonCircuit: Compares two values based on the operator and evaluates to 1 if true, 0 if false: '>','<','!','+'
MinMaxCircuit: Extends MathCircuit, evaluates to the the min or max value based on the state: '<','>'
CircuitBlock: Manages multiple circuits using the circuits interface and handles wiring/unwiring.

p3: Driver project for demonstration and testing.

Solution built on JetBrains Rider.

