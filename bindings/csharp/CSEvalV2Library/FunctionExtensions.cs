﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNTK
{
    public static class FunctionExtensions
    {
        public static void Evaluate(this Function func, Dictionary<Variable, Value> arguments, Dictionary<Variable, Value> outputs, DeviceDescriptor computeDevice)
        {
            // Evaluate the rootFunction.
            var argMap = new UnorderedMapVariableValuePtr();
            foreach (var p in arguments)
            {
                argMap.Add(p.Key, p.Value);
            }

            var outMap = new UnorderedMapVariableValuePtr();
            foreach (var p in outputs)
            {
                outMap.Add(p.Key, p.Value);
            }

            func.Evaluate(argMap, outMap, computeDevice);

            foreach (var p in outMap)
            {
                outputs[p.Key] = p.Value;
            }
        }

        public static void Evaluate(this Function func, Dictionary<string, Value> arguments, Dictionary<string, Value> outputs, DeviceDescriptor computeDevice)
        {

            // Evaluate the rootFunction.
            var argMap = new UnorderedMapVariableValuePtr();
            foreach (var p in arguments)
            {
                var variable = func.Arguments.Where(v => string.Equals(v.Name, p.Key)).Single();
                if (variable == null)
                {
                    throw new KeyNotFoundException("No input variable '" + p.Key + "' found.");
                }
                argMap.Add(variable, p.Value);
            }

            var outMap = new UnorderedMapVariableValuePtr();
            foreach (var p in outputs)
            {
                var variable = func.Outputs.Where(v => string.Equals(v.Name, p.Key)).Single();
                if (variable == null)
                {
                    throw new KeyNotFoundException("No output variable '" + p.Key + "' found.");
                }
                outMap.Add(variable, p.Value);
            }

            func.Evaluate(argMap, outMap, computeDevice);

            foreach (var p in outMap)
            {
                outputs[p.Key.Name] = p.Value;
            }
        }
    }
}