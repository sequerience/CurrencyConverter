# CurrencyConverter
Currency Converter - приложение для работы с валютами и получения актуальной информации о котировках. Вот основные функции, которые оно предлагает:

Из API если интернет подключен, из базы данных если интернет не подключен.

1)Вывод доступных валют. Например, пользователь вводит команду --currencies в консольное приложение и ему выводится список доступных валют.

2)Вывод котировки валюты с возможностью выбора даты (если дата не указана, то использовать котировку за текущий день). Например, пользователь вводит команду --quota USD --date 01.12.2021 и ему выводится информация о курсе доллара относительно рубля “Курс USD: 73.92 RUB”.

3)Конвертация из одной валюты в другую с возможностью выбора даты (если дата не указана, то использовать котировку за текущий день). Например, пользователь вводит команду --exchange 10 --from USD to EUR --date 01.12.2021 и ему выводится информация о конвертации валюты “10 USD = 8.82 EUR”.

# XmlApiRequester
XmlApiRequester - дополнительное приложение, которое раз в день делает запрос к XML API центрального банка РФ, парсит информацию и записывает в базу данных.

# Документация к библиотеке CurrencyConverterCore
Библиотека CurrencyConverterCore предоставляет набор классов и функций для работы с конвертером валют. Ниже представлено краткое описание каждого класса и его методов:

### SqlDataAccess

| Метод/Функция | Обязанность | Возврат |
| --- | --- | --- |
| OpenConnection	| Открывает соединение с базой данных	| - |
| CloseConnection	| Закрывает соединение с базой данных	| - |
| CheckEmptyDataBase |	Проверяет, является ли база данных пустой |	bool |
| GetRowCountFromSearch	| Возвращает количество записей, найденных по заданной дате |	int | 
| InsertCurrency |	Вставляет новую запись о валюте в базу данных	| - |
| UpdateCurrency |Обновляет запись о валюте в базе данных	| - |
| SelectConcreteCurrency | Возвращает значение конкретной валюты по её коду и дате	| decimal |
| SelectAllCurrencies	| Возвращает список всех валют, найденных по заданной дате	| List<CurrencyModel |


### SqlDataOperations

| Метод/Функция | Обязанность | Возврат |
| --- | --- | --- |
| UpdateDataBase	| Обновляет базу данных с информацией о валютах на текущую дату	| - |
| AvailableCurrenciesDataBase |	Проверяет наличие валют в базе данных для указанной даты и выводит их список	| - |
| CurrencyQuotaDataBase	| Выводит курс указанной валюты на указанную дату	| - |
| ExchangeCurrenciesDataBase |	Выполняет обмен указанного количества валюты на другую валюту по указанной дате	| - |

### ICurrencyConverter

| Метод/Функция | Обязанность | Возврат |
| --- | --- | --- |
| AvailableCurrencies |	Выводит список доступных валют на указанную дату	| - |
| CurrencyQuota	| Выводит курс указанной валюты на указанную дату	| - |
| CurrencyExchange |	Выполняет конвертацию указанного количества валюты из одной валюты в другую на указанную дату	| - |

### DataProvider
| Метод/функция	| Обязанность |	Возврат |
| --- | --- | --- |
| CurrencyConverter |	Возвращает объект для конвертации валют	| ICurrencyConverter |

### NetworkStatusChecker
| Метод/функция	| Обязанность |	Возврат |
| --- | --- | --- |
| isConnectedToNetwork |	Проверка подключения к сети	| bool |

### URLBuilder
| Метод/функция	| Обязанность |	Возврат |
| --- | --- | --- |
| GetTodayDate() |	Возвращает текущую дату в формате "dd/MM/yyyy"	| Строка с текущей датой в формате "dd/MM/yyyy" |
| GetUrL() |	Возвращает URL-адрес для текущей даты	 | Строка с URL-адресом для текущей даты |
| GetUrl(string date) |	Возвращает URL-адрес для указанной даты	| Строка с URL-адресом для указанной даты |

### StreamingApiRequest
| Метод/функция	| Обязанность |	Возврат |
| --- | --- | --- |
| DailyThread | Запускает ежедневный поток данных API | - |
