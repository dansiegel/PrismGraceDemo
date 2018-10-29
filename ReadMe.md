# Prism Ioc Demo

The Prism team cannot support every container out there. Looking at Ioc Performance Benchmarks you may feel you want to use a non-supported container such as Grace. While Grace's [performance metrics](https://github.com/danielpalme/IocPerformance#basic-features) are fantastic, it currently does not have enough of a following to justify adding it as an official container.

Prism 7's Ioc Abstractions make supporting new containers extremely easy. As you'll see from this demo app, we only have to add a single class implementing IContainerExtension. Instead of inheriting from a PrismApplication we can inherit directly from PrismApplicationBase and pass back our GraceContainerExtension for `CreateContainerExtension()`.

## Popup Plugin Compatible

Because of the container abstraction, the Popup Plugin remains fully compatible with our application while only ever providing that single class.