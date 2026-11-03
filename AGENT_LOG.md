# Agent log

One entry per agent session, newest at the bottom. Be honest: what went wrong is the useful part.

<!-- Copy this block for each session:

## YYYY-MM-DD · <short title>
- Goal:
- Delegated:
- Checkpoints:
- Went wrong:
- Changed by hand:

-->
## 2026-10-31 · C# MCP server implementation
- Goal: Build a manual MCP server in C# without using an MCP SDK.
- Delegated: Used AI assistance to understand MCP, implement JSON-RPC methods, add tools, and debug the server.
- Checkpoints: Implemented `initialize`, `tools/list`, `tools/call`, `find_lecture`, and `get_course_info`. The checker passed all 12/12 tests.
- Went wrong: Initial implementation had C# compilation issues with an implicitly typed array. Gemini CLI connected to the server but remained unresponsive when sending prompts.
- Changed by hand: Created the C# project, implemented the server logic, added course data files, configured `.gemini/settings.json`, and tested the server manually.