# Transfers Selenium-to-Playwright migration

## Scope and acceptance status

Branch: `gpeterthomas-selenium-to-playwright-migration`.

Source revision inspected: `c49f86c03dab93ea49d0e8c016e365c319acaff2`.
Target starting revision: `146f5061da64f21f2868d80b117d3d71b84a2eda`.

This delivery implements the Transfers slice of the enterprise migration. The user explicitly limited this migration to **SFA.DAS.Transfers.UITests**; it does not migrate the rest of the enterprise repository.

All 12 source Transfers feature files are represented in the target. Eleven previously missing files have been added; the existing target transfer-status feature and target-only provider-login feature have been retained. The result is 13 feature files containing 14 discoverable Reqnroll scenarios: 13 source scenarios and the existing provider-login scenario.

The three Transfers apprentice-approval journeys now use the target's ILR-based flow: learner events are generated against the actual receiver account, published through the existing learner-data integration, added by the provider from ILR to a transfer-funded empty cohort, and reviewed through the existing employer/provider approval pages. The connection journeys are unchanged. Compilation and offline binding checks pass; no environment-connected scenario was executed. See [the file-specific review checklist](TRANSFERS-MANUAL-REVIEW.md) for runtime acceptance gates and the TR_03 adaptation.

The source checkout and the target main checkout were inspected read-only. All restores, builds, tests and edits were performed in the isolated target worktree. No pull request was requested or opened.

## Approved architecture retained

| Concern | Target mechanism used |
|---|---|
| Dependency injection | Existing constructor-injected Reqnroll `ScenarioContext`; existing `Set`/`Get<T>` registrations and `ObjectContext`. No additional container or registration pattern. |
| Browser lifecycle | Existing `SFA.DAS.Framework` Playwright hooks create and dispose the browser, browser context and page, and own traces/screenshots. No Selenium driver or replacement browser manager. |
| Pages | Existing `BasePage`, `ApprovalsBasePage`, `VerifyPageAsync`, `RetryHelper` and shared employer navigation. Target namespaces and async `Task` conventions are used. |
| Page switching | The existing `Driver.Page` points to the active role's page. `BasePage` captures that page at construction, so new page objects are constructed after switching employer/provider roles. The saved employer page is restored before account switches. |
| Finance navigation | Existing `InterimFinanceHomePage` is constructed with navigation disabled; its menu navigation is explicitly awaited. No new constructor-driven async navigation. |
| Accounts | Existing `AccountCreationStepsHelper`, employer portal data, account/legal-entity lookup, account selector and cleanup hooks. First/Second/Third accounts are matched by organisation, not database row order. |
| Provider login | Existing `ProviderHomePageStepsHelper`, provider configuration and shared new-tab handling. |
| Apprentice data | Existing `ApprenticeDataHelper`, `Apprenticeship`, `ApprenticeFactory`, `TrainingFactory`, course catalog and ILR event publishing. A narrow explicit-employer overload uses the Transfers receiver's agreement and legal entity; no parallel data or event pipeline is introduced. |
| Bindings | Existing Transfers `reqnroll.json` binding assemblies: Framework, Approvals, EmployerPortal and ProviderLogin. New Transfers bindings remain in the Transfers assembly. |
| Packages | Existing target `net10.0`, Reqnroll 3.3.4, NUnit adapter 6.1.0 and test SDK 18.6.0. No new package, Selenium, SpecFlow, BrowserStack or browser-management dependency. |

Relevant existing scenario-hook ordering is ObjectContext 0, configuration 1, framework helpers 2, Playwright setup 8, Transfers login configuration 12, Approvals configuration 20, employer navigation/data 22/23, levy-funding setup 24/27, and Approvals additional setup 30-34. Browser teardown remains order 100. These hooks were not replaced.

## Source inventory and feature coverage

The source Transfers project has 19 C# files: 13 page classes, three orchestration helpers, two binding classes and configuration setup. Selenium is used through `OpenQA.Selenium`, `By`, `IWebElement`, inherited page interaction/form-completion helpers and the legacy UI framework. The project references the SpecFlow generator/NUnit integration, BrowserStack adapter, Registration, UI.Framework and TestDataExport. Those references were not copied into the target.

The migration replaces element enumeration and synchronous interaction with Playwright locators, scoped row filters, awaited actions, and retrying assertions. No source WebDriver, DriverFactory or WebDriverWait layer was introduced. The two trivial source Transfers base classes were deliberately not recreated.

| Source feature under `Project\Tests\Features` | Target coverage |
|---|---|
| `TR_01_TransfersApprovalJourney.feature` | Added: receiver has one eligible ILR learner; provider adds it to the transfer-funded empty cohort; receiver and sender approve; task transitions and live record. |
| `TR_02_TransfersApprovalJourney.feature` | Added: receiver has two eligible ILR learners; provider adds both to the transfer-funded empty cohort; receiver and sender approve; task transitions and both live records. |
| `TR_03_TransfersApprovalJourney.feature` | Added: one receiver ILR learner, two sender rejections, exact status/task checks, receiver requests provider review without changing the ILR record, then the required provider/receiver/sender approvals and live record. |
| `TR_04_01_TransfersConnectionJourney.feature` | Added: basic sender/receiver connection. |
| `TR_04_02_TransfersConnectionJourney.feature` | Added: existing receiver becomes sender to existing sender. |
| `TR_04_03_TransfersConnectionJourney.feature` | Added: existing receiver connects to a third account. |
| `TR_04_04_TransfersConnectionJourney.feature` | Added: receiver connects back to its sender. |
| `TR_04_05_TransfersConnectionJourney.feature` | Added: connection from another account to an existing receiver. |
| `TR_04_06_TransfersConnectionJourney.feature` | Added: two senders, one receiver and connection-request task counts. |
| `TR_05_TransfersStatusJourney.feature` | Existing target two-scenario implementation retained, including signed/unsigned agreement status checks. |
| `Accessibility\TR_Accessibility.feature` | Added: tagged ILR-based approval journey and older cohort-task wording. The tag is not evidence that an accessibility analyzer ran. |
| `TestDataPreparation\VerifyDbConnection.feature` | Added: existing helper-based catalog checks for the same 21 source databases; not executed. |

The target-only `TR_01_ProviderLogin.feature` is unchanged. Scenario names, steps and tags have been retained, including the source's `recevier` spelling. The older accessibility task phrases have explicit bindings rather than being left undefined.

### Pages and orchestration

The 11 concrete source Transfers pages now have Playwright counterparts under:

`src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\`

They are `FinancePage`, `TransfersPage`, `ConnectWithReceivingEmployerPage`, `WhichEmployerAreYouConnectingWithPage`, `ConfirmConnectionDetailsPage`, `RequestSentConfirmPage`, `TransferConnectionRequestDetailsPage`, `ConnectionConfirmedPage`, `TransferRequestDetailsPage`, `TransferRequestApprovedPage`, and `TransferRequestRejectedPage`.

`src\SFA.DAS.Transfers.UITests\Project\Helpers\TransferConnectionStepsHelper.cs` handles new accounts, role-specific account selection, connection requests, acceptance and two-sided connection assertions.

`src\SFA.DAS.Transfers.UITests\Project\Tests\Steps\CreateAccountTransfersSteps.cs` binds account/connection, approval, rejection/resubmission, status, task and live-record steps. Unsupported account ordinals and mismatched accepting accounts fail explicitly.

`src\SFA.DAS.Approvals.UITests\Project\Helpers\StepsHelper\TransferCohortStepsHelper.cs` prepares ILR apprentices for the actual receiver, publishes them through `LearnerDataOuterApiSteps`, creates the empty transfer-funded cohort, and delegates learner selection and cohort approval to the existing ILR helpers/pages. It preserves the models, expected statuses, sender/receiver task assertions and live-record checks across role changes.

### ILR approval integration

The three apprentice journeys use the existing ILR model/data/event path (`ApprenticeDataHelper`, `ApprenticeFactory`, `TrainingFactory`, `LearnerDataOuterApiSteps.SLDPushDataIntoAS` and the existing service-bus consumer hooks). The generated `Employer` model is scoped to the Transfers receiver's actual organisation, agreement ID, legal-entity ID and registered email so the provider's ILR list and the transfer-funded cohort refer to the same employer. Training is generated as `WaitingToStart`.

The provider adds the first learner through the existing ILR/RPL selection helper and any remaining learners through the existing ILR helper; it then uses the standard provider approval page. Employer review and approval use the standard ILR employer page. Since provider-added learners are returned to the employer for approval, TR_02 and TR_03 now perform that standard receiver approval explicitly before the sender approval task; the source manual-entry route had embedded that employer-approved state in cohort creation. The shared `LearnerRequestsPage` has a cohort-specific opener for the existing Ready to review/With transfer sender lists; no manual-entry page API remains.

The transfer-specific `SelectTransferConnectionPage` and direct-transfer funding selection remain because the ILR journeys still need a transfer-funded cohort. The manual-entry-only pages, partials and methods have been removed. The target's default ILR approval path is not replaced or forked.

TR_03's source receiver-edit step cannot be reproduced by editing the submitted ILR learner in the target approval UI. It is adapted to the target-supported action: the receiver requests provider review of the rejected cohort; the ILR record remains authoritative and unchanged. The journey retains both sender rejection cycles and the final provider, receiver and sender approvals, but does not claim that apprentice data was edited.

Two tightly coupled shared fixes were necessary:

- `AccountCreationStepsHelper.AddNewAccount` records the actual organisation for `OrgType.Default`, rather than the unrelated charity default. Explicit non-default organisation handling is unchanged.
- `TasksHomePage.AssertTaskCount` is now an awaited Playwright assertion instead of commented Selenium code. It checks the Tasks region, exact matching task-item count and an anchored numeric prefix, supports apprentice/learner/cohort wording, and accepts an empty task list for zero expected tasks.

The source database step was implemented locally in Transfers as `VerifyDbConnectionSteps`, reusing the existing target cleanup SQL helpers, exclusions, authentication and logging. Transfers now has a direct TestDataCleanup project reference. No new SQL/authentication abstraction or wholesale TestDataExport project was introduced.

## Validation evidence and limits

Commands were run from the isolated target worktree with .NET SDK 10.0.403. Nonzero build/test exit codes were preserved.

| Check | Result |
|---|---|
| Restored baseline solution build | Succeeded: 85 warnings, 0 errors. |
| Transfers build with migrated bindings/pages | Succeeded: 38 warnings, 0 errors at that validation point. |
| Final full target solution build after the ILR revision | Succeeded: 64 warnings, 0 errors. Warnings are primarily pre-existing; warning totals vary with incremental compilation. |
| Offline `TestCategory=Unittests` run after the ILR revision | 14/14 passed: one binding-coverage case per target feature plus async-return contract. |
| NUnit/Reqnroll discovery | 14 scenario tests plus 14 offline contract tests; no scenarios missing from discovery. |
| Static legacy/dependency scan | No Selenium, WebDriverWait, DriverFactory or SpecFlow references in changed/new code/project configuration. |
| Async/wait review | No new async-void, `.Result`, `.Wait`, fixed UI delays or blocking waits. Existing waits in untouched target helpers remain. |
| `git diff --check` | Passed. |
| External UI, SQL and service-bus acceptance | Not run: no verified safe environment configuration/authorization for these environment-connected journeys. |

Offline commands:

```powershell
dotnet build .\src\SFA.DAS.TestAutomation.sln --no-restore --verbosity quiet
dotnet test .\src\SFA.DAS.Transfers.UITests\SFA.DAS.Transfers.UITests.csproj --no-build --no-restore --filter "TestCategory=Unittests"
dotnet test .\src\SFA.DAS.Transfers.UITests\SFA.DAS.Transfers.UITests.csproj --no-build --no-restore --list-tests
```

The binding contracts load the actual configured binding assemblies and match every current feature step to exactly one applicable regex binding without starting Reqnroll scenario hooks. They also check that Transfers UI bindings return `Task`. They do not prove browser selectors, application behavior, hooks, external data, or end-to-end approval compatibility.

The solution still reports existing compiler warnings and NuGet NU1902 advisories for `System.Security.Cryptography.Xml` 4.5.0. No dependency-security remediation is included in this migration.

## Broader repository inventory: explicitly not completed

Read-only project inventory found **62 source project files and 40 target project files**. Twenty-nine share exact names; 33 source names have no exact-name target project. Name differences can reflect renaming/consolidation and are not, on their own, evidence that every associated feature is absent.

Only Transfers and its necessary shared approval/employer capabilities were implemented in this delivery. Other existing target projects were not overwritten, and their source-to-target feature parity was not certified. In particular, Campaigns was inspected earlier but no Campaigns migration edits were made.

These are the 33 source project names without an exact-name target counterpart, grouped to make the remaining inventory concrete:

| Source projects (all prefixed `SFA.DAS.`) | Disposition in this delivery |
|---|---|
| `UI.Framework`, `UI.FrameworkHelpers`, `API.FrameworkHelpers`, `MailosaurAPI.Service` | Existing target framework/helpers/services are used instead. No wholesale legacy-framework port; broader helper parity not certified. |
| `Registration.UITests` | Existing EmployerPortal capabilities reused only as needed for Transfers. Remaining Registration feature parity not audited. |
| `Roatp.UITests`, `RoatpAdmin.UITests`, `RoatpAdmin.Service` | Target has Apar-named projects. Potential renamed coverage; not migrated or certified here. |
| `FAT.UITests`, `EmployerFinance.APITests` | Target has FATe/Finance-named projects. Potential renamed/consolidated coverage; not certified here. |
| `TestDataExport` | Only Transfers' required database-verification binding was ported; the rest of the source export project is outside this delivery. |
| `AODP.UITests`, `ApprenticeCommitments.UITests`, `ApprenticeshipDetails.UITests`, `ConsolidatedSupport.UITests` | No exact-name counterpart; remaining source scope, not migrated in this delivery. |
| `EmployerIncentives.UITests`, `EmployerProviderRelationships.UITests`, `RAAQA.UITests`, `SupportConsole.UITests`, `TransferMatching.UITests` | No exact-name counterpart; remaining source scope, not migrated in this delivery. |
| `ApprenticeCommitments.APITests`, `AssessorCertification.APITests`, `Courses.APITests`, `EarlyConnect.APITests` | No exact-name counterpart; outside this Transfers migration. |
| `EmplomentChecks.APITests`, `FAT.APITests`, `ProvideFeedback.APITests`, `TransferMatching.APITests` | No exact-name counterpart; outside this Transfers migration. The source's `EmplomentChecks` spelling is retained in this inventory. |
| `Approvals.ServiceBusIntegrationTests`, `DataAnalytics.Tests`, `FlexiPayments.E2ETests`, `Live.SmokeTests`, `PayeCreation.DataPreparationTests` | No exact-name counterpart; separate integration/data/live scope, not migrated here. |

The safe continuation for any later project is to compare business scenarios with existing target counterparts before editing shared code, preserve the current ILR approval path, and validate both binding discovery and solution compilation. No other project migration or PR is bundled into this branch.
