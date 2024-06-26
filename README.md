# CRUD Library

CRUD Library - это веб-приложение, разработанное на платформе ASP .NET MVC, которое обеспечивает удобную работу с информацией о книгах. 
Приложение предоставляет следующий функционал:
- **Создание**: Пользователи могут добавлять новые записи о книгах, указывая различные атрибуты, такие как название, автор, жанр и т.д.
- **Чтение**: Просмотр списка всех книг, а также детальная информация о каждой книге.
- **Обновление**: Возможность редактирования информации о книге, включая изменение названия, автора, жанра и других атрибутов.
- **Удаление**: Администратор может удалять книги из базы данных.
- **Пагинация и фильтрация**: Для удобства навигации по большому количеству книг реализована пагинация страниц. Также пользователи могут применять фильтры для быстрого поиска книг по различным критериям, таким как название, автор, жанр.
- **Добавление комментариев**: Пользователи могут оставлять комментарии к книгам на их страницах. Для этого предусмотрена специальная форма внизу страницы каждой книги.
- **Удаление комментариев**: Администраторы имеют право удалить комментарии, нарушающие правила сообщества или содержащие нецензурные выражения. Для этого администратору доступны дополнительные функции управления комментариями в административной области приложения.


+ В приложении реализована аутентификация пользователей по логину и паролю, а также разграничение прав доступа между обычными пользователями и администраторами.
+ В приложении реализована возможность оставлять комментарии к книгам. Пользователи могут оставлять свои отзывы и замечания о прочитанных книгах, что позволяет создать активное сообщество пользователей и обменяться мнениями о литературных произведениях.

## Технологии

- Язык программирования: C#
- Фреймворк: ASP .NET MVC
- JavaScript библиотека: jQuery
- CSS фреймворк: Bootstrap
- ORM: Entity Framework
- Принципы SOLID

### Роли пользователей
- **Пользователи**: Могут просматривать список книг и информацию о каждой книге. Для входа в интерфейс пользователей используется обычная авторизация.
- **Администраторы**: Имеют дополнительные права доступа, включая возможность управления книгами (добавление, удаление, обновление). Вход в интерфейс администраторов осуществляется через специальную административную область (Area).

### Структура проекта

- **Areas**: В этой директории размещена административная область приложения, предоставляющая доступ к функциям управления книгами и комментариями.
- **Controllers**: Контроллеры приложения, отвечающие за обработку HTTP-запросов и координацию работы с моделями и представлениями.
- **Views**: Представления, обеспечивающие отображение пользовательского интерфейса и форм для работы с книгами.
- **Models**: Классы моделей, представляющие собой сущности книг и пользователей.
- **Scripts** и **Styles**: Файлы JavaScript и CSS для улучшения пользовательского интерфейса и стилевого оформления.
- **Services**: Сервисы, обеспечивающие бизнес-логику и взаимодействие с базой данных.
