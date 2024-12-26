﻿namespace HfDemo.DomainModel;

public class CalculationJob
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }

    public string Status { get; set; }

    public string CalculationParams { get; set; }
    
    public string CalculationResult { get; set; }
}
