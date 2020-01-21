﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Data.SqlTypes;
using Microsoft.Bot.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Converters
{
    /// <summary>
    /// Converter which allows json to be expression to object or static object.
    /// </summary>
    /// <typeparam name="T">The type of the items of the array.</typeparam>
    public class ArrayExpressionConverter<T> : JsonConverter<ArrayExpression<T>>
    {
        public override bool CanRead => true;

        public override ArrayExpression<T> ReadJson(JsonReader reader, Type objectType, ArrayExpression<T> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.ValueType == typeof(string))
            {
                return new ArrayExpression<T>((string)reader.Value);
            }
            else
            {
                return new ArrayExpression<T>(JToken.Load(reader));
            }
        }

        public override void WriteJson(JsonWriter writer, ArrayExpression<T> value, JsonSerializer serializer)
        {
            if (value.Expression != null)
            {
                serializer.Serialize(writer, value.ToString());
            }
            else
            {
                serializer.Serialize(writer, value.Value);
            }
        }
    }
}
