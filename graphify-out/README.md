# Pre-built code graph (graphify)

Built from `main` with `graphify update .` (code-only, tree-sitter, no LLM, nothing left the machine).

- `GRAPH_REPORT.md` - summary: god nodes, communities, surprising connections
- `graph.html` - open in a browser to explore
- `graph.json` - what the CLI and the agent query

Use it if you cannot install graphify on your machine. It is a snapshot: it goes stale the moment the code changes.
Rebuild: `pip install graphifyy` then `graphify update .`
