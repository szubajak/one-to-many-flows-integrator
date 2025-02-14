# OneToManyMultiFlows

Template which can help you start writing .NET 9 Api to integrate with many providers using many flows

How it works?

* Flow has it's own controller with requestDto and responseDto to decompose logic and simplify development and collaboration
* Flow can support 1 or more handlers based on requested provider
* Request handlers are registered using .NET 9 build-in service container, based on provider
