﻿namespace Ambev.DeveloperEvaluation.Domain.Common;

public abstract class QueryParamsBase
{
    public string? OrderBy { get; set; }
    public bool OrderDescending { get; set; } = false;
    public int? Skip { get; set; }
    public int? Take { get; set; }
}

