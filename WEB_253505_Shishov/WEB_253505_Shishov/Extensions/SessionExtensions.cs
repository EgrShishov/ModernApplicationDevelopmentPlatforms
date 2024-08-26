using System.Text.Json;

namespace WEB_253505_Shishov.Extensions;

public static class SessionExtensions
{
	public static void Set<T>(this ISession session, string key, T value)
	{
		session.SetString(key, JsonSerializer.Serialize(value));
	} 
	public static T? Get<T>(this ISession session, string key)
	{
		session.TryGetValue(key, out var serializedValue);
		return serializedValue == null ? default(T) : JsonSerializer.Deserialize<T>(serializedValue);
	}
}
