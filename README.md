# Roadwise – sistem za upravljanje autoškolom

Seminarski rad iz predmeta Razvoj softvera I (2026/27). Web aplikacija za instruktore i kandidate autoškole s dva poslovna procesa:

- **Flow 1, časovi vožnje i uplate:** paketi obuke, uplate, termini, rezervacije, evidencija časova, prijava za praktični ispit.
- **Flow 2, testovi i materijali:** testovi i pitanja, rješavanje i bodovanje, video lekcije i dokumenti.

Plan rada je na GitHub Projects tabli [Roadwise](https://github.com/orgs/2A-Labs/projects/1). Dokumentacija je u folderu [`dokumenti/`](dokumenti/):
- istraživanje sličnih aplikacija
- mind map arhitekture

Aplikacija je napravljena na početnom projektu predmeta (Market). Iz njega su uklonjeni dijelovi web shopa, a ostala je osnova na kojoj se gradi Roadwise.

| Folder | Sadržaj |
|---|---|
| [`Roadwise.Backend/`](Roadwise.Backend/) | ASP.NET Core Web API, .NET 10, Clean Architecture, CQRS (MediatR), FluentValidation, EF Core (SQL Server), JWT + refresh tokeni |
| [`Roadwise.Frontend/`](Roadwise.Frontend/) | Angular 21 (NgModules), Angular Material, ngx-translate (bosanski/engleski) |
| [`dokumenti/`](dokumenti/) | Dijagrami, screenshotovi, mind map arhitekture |
| [`db-backups/`](db-backups/) | Backup baze (zip) |

## Preduslovi

- .NET 10 SDK (10.0.100 ili noviji; verzija je zadana u `Roadwise.Backend/global.json`)
- Visual Studio 2026 (v18) ili noviji, Rider 2025.3+ ili VS Code sa C# Dev Kit
- Node.js `^20.19`, `^22.12` ili `>= 24` (preporuka: 22 ili 24 LTS)
- SQL Server na `localhost,1433`, korisnik `sa`, lozinka `test` (connection string je u `Roadwise.Backend/Roadwise.API/appsettings.json`)
- Repozitorij u kratkom folderu (npr. `C:\rs1`) i jednom: `git config --global core.longpaths true`

## Pokretanje backenda

Jednom na računaru (HTTPS certifikat za localhost):

```bash
dotnet dev-certs https --trust
```

Iz foldera `Roadwise.Backend`:

```bash
dotnet run --project Roadwise.API --launch-profile https
```

- API: `https://localhost:7100` (i `http://localhost:5100`)
- Swagger: `https://localhost:7100/swagger`
- Baza `Roadwise` se pri pokretanju sama kreira i migrira, a demo podaci se upisuju u okruženjima `Development` i `Testing`.

## Pokretanje frontenda

Iz foldera `Roadwise.Frontend`:

```bash
npm ci
npm start
```

Aplikacija: `http://localhost:4200`. Backend mora biti pokrenut.

Ako HTTPS certifikat ne radi, pokrenite backend s profilom `http` i u `src/environments/environment.development.ts` stavite `apiUrl: 'http://localhost:5100'`.

## Korisnici (demo podaci)

| Email | Lozinka | Uloga |
|---|---|---|
| `admin@roadwise.local` | `Admin123!` | administrator (`/admin`) |
| `manager@roadwise.local` | `Manager123!` | menadžer |
| `employee@roadwise.local` | `Employee123!` | uposlenik |
| `kandidat@roadwise.local` | `Kandidat123!` | korisnik bez uloge (`/client`) |
| `string` | `string` | za Swagger |
| `test` | `test123` | za testove |

Uloge su za sada one iz početnog projekta. Uloge **instruktor** i **kandidat** uvodi issue #1.

Administrator nakon prijave ide na `/admin`, svi ostali na `/client`. Novi korisnik se može registrovati na `/auth/register`. Lozinka mora imati najmanje 8 znakova, bar jedno slovo i jednu cifru.

## Okruženja (environments)

| | Development | Testing | Production |
|---|---|---|---|
| Namjena | svakodnevni razvoj | lokalno, ponaša se kao produkcija | server |
| Backend profil | `https` / `http` | `https-testing` | – |
| Frontend | `npm start` | `npm run start:testing` | `npm run build` |
| Baza | `Roadwise` | `Roadwise_Testing` | iz konfiguracije servera |
| Demo podaci | da | da | ne |
| Swagger | da | ne | ne |
| Detalji greške (stack trace) u odgovoru | da | ne | ne |
| JWT ključ | `DEV_ONLY_…` | `TESTING_ONLY_…` | tajni ključ u `Jwt__Key` |
| CORS | `http://localhost:4200` | `http://localhost:4200` | iz konfiguracije servera |
| Email | ispis u log (`[DEV EMAIL]`) | ispis u log | SMTP (`Email__Smtp__Host`…) |

`appsettings.json` važi za sva okruženja, a `appsettings.Development.json` / `appsettings.Testing.json` ga dopunjavaju. Produkcijske tajne (`Jwt__Key`, SMTP) dolaze iz varijabli okruženja i nikad nisu u repozitoriju. Automatski testovi koriste okruženje `IntegrationTests` (in-memory baza).

Na frontendu se uvijek importuje `environments/environment`; `environment.development.ts` i `environment.testing.ts` ga zamjenjuju preko `fileReplacements`.

## Migracije

Iz foldera `Roadwise.Backend` (prvi put `dotnet tool restore` instalira `dotnet-ef` iz `dotnet-tools.json`):

```bash
dotnet tool restore
dotnet ef migrations add NazivMigracije -p Roadwise.Infrastructure -s Roadwise.API -o Migrations
```

Package Manager Console (Default project: `Roadwise.Infrastructure`, startup projekat `Roadwise.API`): `Add-Migration NazivMigracije`. Baza se ažurira sama pri pokretanju API-ja.

## Testovi

```bash
dotnet test
```

## Šta je u osnovi (iz početnog projekta)

| Dio | Backend | Frontend |
|---|---|---|
| Prijava, refresh tokeni, odjava, registracija, zaboravljena i nova lozinka, moj profil | `Modules/Auth`, `AuthController` | `modules/auth`, `modules/account`, `core/services/auth` |
| Valute i kursna lista s historijom (bazna valuta KM, EUR fiksno 1,95583) | `Modules/Finance`, `CurrenciesController`, `ICurrencyConverter` | `admin/finance/exchange-rates`, `public/converter`, izbor valute u headeru, `money` pipe |
| Upload fajlova u `wwwroot/uploads` (provjera tipa i veličine) | `IFileStorage`, `LocalFileStorage` | – |
| Email (u razvoju se ispisuje u log) | `IEmailSender` | – |
| Keš (memorija ili Redis, trenutno se ne koristi) | `ICacheService` | – |
| Admin početna (mjesto za dashboard), postavke, kursna lista | – | `/admin`, `/admin/settings`, `/admin/exchange-rates` |
| Početna za korisnika | – | `/client` |

Uplate u više valuta, kurs koji se pamti uz transakciju i povrat novca dio su issuea #8.

### Šta je uklonjeno iz početnog projekta

Katalog (kategorije, proizvodi, promocije, favoriti), narudžbe, korpa, checkout i pretraga proizvoda: entiteti, moduli, kontroleri, testovi, ekrani, API servisi i prevodi. Prijašnja `Init` migracija je zamijenjena novom koja sadrži samo tabele `Users`, `RefreshTokens`, `PasswordResetTokens`, `Currencies` i `ExchangeRates`.

Primjeri formi iz početnog projekta (forma na stranici, dijalog, async validator, upload slike, FormArray, autocomplete) bili su na ekranima kataloga i narudžbi. Ostaju dostupni u repozitoriju predmeta (`adil-fit-ba/rs1-2026-27`, folder `starter`) kao uzor.

## Tok rada

Po tački 8 uputa: issue (Backlog → Ready, procjena u satima) → grana `broj-opis` → implementacija i testovi → pull request s `Closes #broj` → code review komentar → merge.
