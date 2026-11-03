using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

string? line;

while ((line = Console.ReadLine()) != null)
{
    Console.Error.WriteLine($"got: {line}");

    JObject request = JObject.Parse(line);
    string? method = (string?)request["method"];

    if (method == "initialize")
    {
        var response = new
        {
            jsonrpc = "2.0",
            id = request["id"],
            result = new
            {
                protocolVersion = (string?)request["params"]?["protocolVersion"],
                capabilities = new
                {
                    tools = new { }
                },
                serverInfo = new
                {
                    name = "lectures",
                    version = "0.1.0"
                }
            }
        };

        Console.WriteLine(JsonConvert.SerializeObject(response));
    }
    else if (method == "tools/list")
    {
        var response = new
        {
            jsonrpc = "2.0",
            id = request["id"],

            result = new
            {
                tools = new object[]
                {
                    new
                    {
                        name = "find_lecture",
                        description = "Helps with course-related questions.",
                        inputSchema = new
                        {
                            type = "object",
                            properties = new
                            {
                                number = new
                                {
                                    type = "integer",
                                    description = "The number of the lecture, from 1 to 12."
                                }
                            },
                            required = new[] { "number" }
                        }
                    },

                    new
                    {
                        name = "get_course_info",
                        description = "Returns general information about the course using locally stored course data.",
                        inputSchema = new
                        {
                            type = "object",
                            properties = new { }
                        }
                    }
                }
            }
        };

        Console.WriteLine(JsonConvert.SerializeObject(response));

    }
    else if (method == "tools/call")
    {
        string? toolName = (string?)request["params"]?["name"];

        if (toolName == "find_lecture")
        {
            int lectureNumber =
                (int)request["params"]!["arguments"]!["number"]!;

            string json = File.ReadAllText("mcp-server/lectures.json");

            JArray lectures = JArray.Parse(json);

            JToken? lecture = lectures.FirstOrDefault(
                l => (int)l["number"]! == lectureNumber
            );

            if (lecture != null)
            {
                string title = (string)lecture["title"]!;
                string topics = (string)lecture["topics"]!;

                string resultText =
                    $"Lecture {lectureNumber}: {title}. Topics: {topics}";

                var response = new
                {
                    jsonrpc = "2.0",
                    id = request["id"],
                    result = new
                    {
                        content = new[]
                        {
                            new
                            {
                                type = "text",
                                text = resultText
                            }
                        },
                        isError = false
                    }
                };

                Console.WriteLine(JsonConvert.SerializeObject(response));
            }
            else
            {
                var response = new
                {
                    jsonrpc = "2.0",
                    id = request["id"],
                    result = new
                    {
                        content = new[]
                        {
                            new
                            {
                                type = "text",
                                text = $"There is no lecture {lectureNumber}. The course has lectures 1 to 10."
                            }
                        },
                        isError = true
                    }
                };

                Console.WriteLine(JsonConvert.SerializeObject(response));
            }
        }
        else if (toolName == "get_course_info")
        {
            string courseInfo = File.ReadAllText("mcp-server/course-info.txt");

            var response = new
            {
                jsonrpc = "2.0",
                id = request["id"],
                result = new
                {
                    content = new[]
                    {
                        new
                        {
                            type = "text",
                            text = courseInfo
                        }
                    },
                    isError = false
                }
            };

            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
        else
        {
            var response = new
            {
                jsonrpc = "2.0",
                id = request["id"],
                error = new
                {
                    code = -32602,
                    message = $"Unknown tool: {toolName}"
                }
            };

            Console.WriteLine(JsonConvert.SerializeObject(response));
        }
    }
    else if (method != "notifications/initialized")
    {
        var response = new
        {
            jsonrpc = "2.0",
            id = request["id"],
            error = new
            {
                code = -32601,
                message = $"Unknown method: {method}"
            }
        };

        Console.WriteLine(JsonConvert.SerializeObject(response));
    }
}
