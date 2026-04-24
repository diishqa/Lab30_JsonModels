# Лабораторная работа №30. JSON и модели данных
## Кузьмина Диана ИСП-232
___
| Атрибут / настройка | Что делает | Пример |
|---------------------|------------|--------|
| [JsonPropertyName("name")] | Задаёт имя поля в JSON | "name" вместо "Name" |
| [JsonIgnore] | Поле не попадает в JSON | Пароли, внутренние поля |
| PropertyNamingPolicy.CamelCase | Все поля в camelCase | "realName" вместо "RealName" |
| JsonStringEnumConverter | Enum как строка | "Marvel" вместо 0 |
| WriteIndented = true | JSON с отступами | Для удобства при разработке |