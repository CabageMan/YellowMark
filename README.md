# YellowMark

A .Net application implementing the notice board functionality for SolarLab's .Net educational course.

## Running

Application uses User-Secrets in Development mode.
```
{
    "DbConnection": {
        "Username": <username>,
        "Password": <password>
    }
}
```

### Linux
In the src/Hosts/YellowMark.Api directory run:

```
dotnet run --environment Development
```