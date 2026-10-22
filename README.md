# Acme Integration: Integration Engineer Case

Starter repository for the Acme case. Open [`docs/case.html`](docs/case.html) in a browser for the full brief: the situation, the payload, and Acme's requests.

## Getting started

Requires the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet test                                   # one endpoint test, run against every sample
dotnet run --project src/Acme.Integration.Api # starts the API
```

Try it:

```bash
curl -X POST http://localhost:5046/transform \
  -H "Content-Type: application/json" \
  -d @samples/order-denmark.json
```

Right now `POST /transform` returns the payload unchanged.

## What's here

| Path | What it is |
|---|---|
| `src/Acme.Integration.Api` | Minimal API with `POST /transform`, payload models (snake_case JSON) and `OrderTransformer`, where your work starts |
| `tests/Acme.Integration.Tests` | Endpoint test that runs every sample through `/transform` |
| `reference/sales-areas.json` | Acme's sales areas |
| `samples/` | Example payloads |
| `docs/case.html` | The case brief |

## Submitting

1. Click **Use this template → Create a new repository** and make it **private**.
2. Do your work there, including `SOLUTION.md` and `QUESTIONS.md` in the repo root.
3. Add **thomas@goautonomous.io** as a collaborator at least one working day before your interview.

Feel free to restructure anything. The starter is a convenience, not a constraint.
