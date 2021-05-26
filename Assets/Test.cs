using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Quantum.Diagnostics;
using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using UnityEngine;
using quantum;

static class IEnumerableExtensions
{
    public static bool Parity(this IEnumerable<bool> bitVector) =>
        bitVector.Aggregate(
            (acc, next) => acc ^ next
        );

    public static string ToDelimitedString(this IEnumerable<bool> values) =>
        String.Join(", ", values.Select(x => x.ToString()));
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        UnitySystemConsoleRedirector.Redirect();

        var bits = new[] { false, true, false };
        using (var sim = new QuantumSimulator())
        {
            Console.WriteLine($"Input: {bits.ToDelimitedString()}");

            var restored = await RunAlgorithm.Run(sim, new QArray<bool>(bits));
            Console.WriteLine($"Output: {restored.ToDelimitedString()}");

            Debug.Assert(bits.Parity() == restored.Parity());
        }
    }
}
