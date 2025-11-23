🌍 [Leia em Português](README.pt-BR.md)

# Skill Bridge Api Rest

API REST developed as ASP.NET Core and OracleDB + EF Core to make persistense in the project skill bridge.

## Routes

(Users)

- `GET api/v1/users` - Get all users.
- `GET api/v1/users/{id}` - Get user by id.
- `POST api/v1/users` - Create a new user.
- `PUT api/v1/users/{id}` - Update user by id.
- `DELETE api/v1/users/{id}` - Delete user by id.

(Skills)

- `GET api/v1/skills` - Get All skills.
- `GET api/v1/skills/{id}`- Get skill by Id.
- `POST api/v1/skills` - Create a New skill.
- `PUT api/v1/skills/{id}` - Update skill by Id.
- `DELETE api/v1/skills/{id}` - Delete skill by Id.

(Course)

- `GET api/v1/courses/{id}` - Get Course by id.
- `GET api/v1/courses` - Get All Courses.
- `POST api/v1/courses` - Create a New Course.
- `PUT api/v1/courses/{id}` - Update Course by Id.
- `DELETE api/v1/courses/{id}` - Delete Course by Id.

(Work)

- `GET api/v1/works/{id}` - Get work by id.
- `GET api/v1/works` - Get All works.
- `POST api/v1/works` - Create a New work.
- `PUT api/v1/works/{id}` - Update work by Id.
- `DELETE api/v1/works/{id}` - Delete work by Id.

(LearningPath)

- `GET api/v1/learning-paths/{id}` - Get work by id
- `GET api/v1/learning-paths` - Get All learning-paths.
- `POST api/v1/learning-paths` - Create a New learning-paths.
- `PUT api/v1/learning-paths/{id}` - Update learning-paths by Id.
- `DELETE api/v1/learning-paths/{id}` - Delete learning-paths by Id.

(Recommendation)

- `GET api/v1/recommendations/{id}` - Get recommendation by id.
- `GET api/v1/recommendations` - Get All recommendations.
- `POST api/v1/recommendations` - Create a New recommendation.
- `PUT api/v1/recommendations/{id}` - Update recommendation by Id.
- `DELETE api/v1/recommendations/{id}` - Delete recommendation by Id.

(More and etc)

- `GET api` - Get api description.
- `GET healthz` - Get the healthchecks.

## Steps to run

1. Clone the repository:

```bash
git clone https://github.com/felipeclarindo/skill-bridge-api-rest-dotnet.git
```

2. Enter repository:

```bash
cd skill-bridge-api-rest-dotnet
```

3. Create and configure the `.env` file using the model in [.env.example](./.env.example)

4. Enter in Api Directory:

```bash
cd ./Src/WebApi
```

5. Run migrations:

```bash
dotnet ef database update
```

6. Run the api:

```bash
dotnet run
```

7. The api is avaible on:

- <http://localhost:5272/api/v1>

8. To run the tests, execute the command bellow in the root path:

```bash
dotnet test
```

## Team

- RM: 554547 -> Felipe Gabriel Lopes Pinheiro Clarindo
- RM: 558482 -> Humberto de Souza Silva
- RM: 554914 -> Vinicius Beda de Oliveira

## Contribution

Contributions are welcome! If you have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

This project is licensed under the [GNU Affero License](https://www.gnu.org/licenses/agpl-3.0.html).
