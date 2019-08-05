# OneToManyMultiFlowsIntegrator

This template can help you start writing .Net Core microservice to integrate with many providers using many flows.

Real world scenario:
You want to integrate your system with third party providers to process for example shippings or payments.
Your system is used world wide.
In different countries you have to integrate with various, local providers.
You want to use many flows, like GetPaymentDetails, SendPayment, CancelPayment and so on.
Some providers support some flow or not.

Architecture overview:

![alt text](https://github.com/szubajak/one-to-many-flows-integrator/blob/master/overview.jpg)
