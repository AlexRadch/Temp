// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

_ = BenchmarkRunner.Run(
    typeof(Program).Assembly, 
    ManualConfig.Create(DefaultConfig.Instance)
        .WithOptions(ConfigOptions.JoinSummary),
    args);
