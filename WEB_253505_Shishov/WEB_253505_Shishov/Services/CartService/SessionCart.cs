using System.Text.Json.Serialization;
using WEB_253505_Shishov.Domain.Entities;
using WEB_253505_Shishov.Extensions;

namespace WEB_253505_Shishov.Services.CartService;

public class SessionCart : Cart
{
	[JsonIgnore]
	public ISession? Session { get; set; }
	public static Cart GetCart(IServiceProvider services)
	{
		ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
		SessionCart cart = session.Get<SessionCart>("cart") ?? new SessionCart();
		cart.Session = session;
		return cart;
	}
	public override void AddToCart(Constructor constructor)
	{
		base.AddToCart(constructor);
		Session?.Set("cart", this);
	}
	public override void RemoveItems(int id)
	{
		base.RemoveItems(id);
		Session?.Set("cart", this);
	}
	public override void ClearAll()
	{
		base.ClearAll();
		Session?.Remove("cart");
	}
}
