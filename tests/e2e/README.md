# E2E tests (Playwright)

```bash
cd tests/e2e
npm install
npx playwright install chromium
npx playwright test             # starts the API automatically (dotnet run) if it is not running
```

The API must be reachable on http://localhost:5080. Tests share one running server, so a test that analyzes a study changes what the next test sees.
