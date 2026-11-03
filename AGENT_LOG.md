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
- Delegated: Used AI assistance to understand MCP, implement JSON-RPC methods, add tools, and debug the server. Used Gemini CLI to test tool discovery and tool usage.
- Checkpoints: Implemented `initialize`, `tools/list`, `tools/call`, `find_lecture`, and `get_course_info`. The checker passed all 12/12 tests.
- Went wrong: The initial implementation had C# compilation issues with an implicitly typed array. Gemini CLI connected to the server and discovered both tools, but became unresponsive when sending prompts. The /stats command could not be completed..
- Changed by hand: Created the C# project, implemented the server logic, added course data files, configured `.gemini/settings.json`, and tested the server manually.