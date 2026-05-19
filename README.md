# OneToManyMultiFlowsIntegrator  
A clean .NET Core template for integrating with many providers using many flows

**OneToManyMultiFlowsIntegrator** is a lightweight template that helps you build **modular, testable, multi‑provider integrations** in .NET Core.  
It is designed for real-world scenarios where your system must support **multiple external providers** and **multiple business flows**, each with different capabilities, request/response formats, and rules.

Typical use cases include:

- Payment providers  
- Shipping/logistics providers  
- Notification providers  
- Any external API where **one flow** can be executed by many providers, but not all providers support all flows  

This template gives you a clean, extensible starting point for building such integrations without ending up with a giant `switch(provider)` or duplicated logic.

Each flow is isolated into its own folder with:

- A **Controller**
- A **Request DTO**
- A **Response DTO**
- A **Handler interface**
- One or more **Provider-specific handler implementations**

This structure ensures:

- Clear separation of concerns  
- Easy testability  
- Plug‑and‑play provider support  
- No cross‑flow coupling  
- No provider-specific logic leaking into controllers  

This structure makes it easy to:

- Add new flows  
- Add new providers  
- Test each provider/flow combination independently  

---

## 🔄 How the Flow Execution Works

### **1. The client calls a flow endpoint**

Each flow has its own controller, e.g.:

```
POST /api/payment/send
POST /api/payment/cancel
POST /api/shipping/create
```

The request includes the **provider identifier**, e.g. `"DHL"`, `"Stripe"`, `"PayPal"`.

---

### **2. The controller resolves the correct handler**

Each flow defines an interface, for example:

```csharp
public interface ISendPaymentHandler
{
    Task<SendPaymentResponse> HandleAsync(SendPaymentRequest request);
}
```

Providers implement this interface:

```
StripeSendPaymentHandler
PayPalSendPaymentHandler
LocalBankSendPaymentHandler
```

Handlers are registered in DI.

At runtime, the controller selects the correct handler based on the provider.

### **3. Provider-specific logic executes**

Each handler contains only the logic for its provider:

- Mapping request DTO → provider API format  
- Calling the provider  
- Mapping provider response → flow response DTO  
- Handling provider-specific errors  

This keeps your controllers clean and your provider logic isolated.

---

## 🧪 Testing

The template is built for testability:

- Each provider handler can be unit tested independently  
- Flow controllers can be tested with mocked handlers  
- No shared state or cross-provider dependencies  
- No branching logic inside controllers  

Your included test project (`OneToManyFlows.Tests`) demonstrates this pattern.

---

## 🌍 Real-World Example

Imagine you support multiple payment providers:

- Stripe  
- PayPal  
- LocalBank  
- Klarna  

And multiple flows:

- `GetPaymentDetails`  
- `SendPayment`  
- `CancelPayment`  

Not every provider supports every flow.  
Not every provider uses the same request/response format.

This template gives you a clean, scalable way to model that reality.

---

## 🧱 Why This Template Exists

Because real integrations are messy:

- Providers differ  
- Flows differ  
- Capabilities differ  
- Error models differ  
- DTOs differ  

This template gives you:

- A predictable folder structure  
- A clean separation between flows and providers  
- A DI-driven handler resolution mechanism  
- A testable, maintainable architecture  

It’s a practical starting point for any multi-provider integration microservice.

---

## 📄 License

MIT License — free to use in commercial and open-source projects.
