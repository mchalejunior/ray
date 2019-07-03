IMPORTANT:
Bleeding a little bit, getting used to the xunit.gherkin.quick framework.

## Main issues

1. It's mostly plain text in the Feature files, but there are some special characters
   to be aware of:
   + < ( ) \
   It's mostly because you then copy this text to the unit test attribute,
   and here it is used to form a regex. So any character that can cause trouble there
   should be avoided.

2. Feature files are very sensitive to character encoding.
   In short: Any problems having your test discovered, just copy a known good file
   and use it as a starting place. Edit manually and avoid copy-pasting from PDFs etc.
   Sounds simple, but this cost a lot of time:
   https://github.com/ttutisani/Xunit.Gherkin.Quick/issues/82

3. Minus numbers captured as (-\d) in the test attribute. This isn't great, because
   ideally the Feature file could feed any number to the test. But with this syntax,
   you're forced to specify negative numbers up-front and can't change this with the
   same binding.

4. In general (to learn more about xunit.gherkin.quick / Gherkin syntax): 
   Trick seems to be to study the code base - the documentation is incomplete. 
   E.g. see [test files and matching feature files](https://github.com/ttutisani/Xunit.Gherkin.Quick/blob/master/source/Xunit.Gherkin.Quick.ProjectConsumer/Addition/AddNumbersTo5.cs)
