# Transfers migration: manual review and acceptance gates

Implementation summary: [TRANSFERS-MIGRATION.md](TRANSFERS-MIGRATION.md).
Branch: `gpeterthomas-selenium-to-playwright-migration`.

The source scenarios, their bindings and needed page interactions have been implemented and compile. The following files still require review against a known-safe deployed environment. No live transfer, account creation, database catalog query, approval or service-bus flow was executed. A build or binding-discovery pass must not be interpreted as end-to-end acceptance.

## Required manual-entry compatibility decision

The target Approvals project is ILR-oriented; source Transfers relies on manual entry. The user explicitly included the necessary shared capabilities. Those capabilities have been added separately, using source-backed controls and existing target navigation/data/lifecycle.

It has **not** been established that the current application removed manual entry. Check the actual deployed application before accepting the three approval journeys and the tagged accessibility journey. If it no longer exposes the source-backed direct-transfer/manual-entry controls, record the exact missing route/control and affected scenarios as an incompatibility. Do not invent selectors, skip the action, return success, or quietly replace the journey with an ILR upload.

## Shared approval files requiring review

| Exact repository-relative path | Concrete review reason |
|---|---|
| `src\SFA.DAS.Approvals.UITests\Project\Helpers\StepsHelper\TransferCohortStepsHelper.cs` | Verify employer/provider page restoration, sender/receiver selection, cohort references and all three approval/rejection flows. The target's generated training data replaces the legacy data helpers; confirm the selected course, start date and prices are valid for this environment. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\ChooseFundingPage.cs` | Confirm direct-transfer option `#FundingType-1` still leads to connection selection. Existing ILR/reservation methods are unchanged. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\SelectTransferConnectionPage.cs` | Confirm the sender's organisation is the radio's accessible name and the specified sender, rather than an arbitrary first connection, is selected. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\HowWouldYouLikeToAddLearnersPage.cs` | Confirm employer manual entry and provider-added request still exist, including source `message` input and the confirmation's sent-message value. Default ILR actions are retained. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\ManualApprenticeshipCoursePage.cs` | Confirm employer autocomplete and provider standard dropdown are available on manual routes, and select the intended course. Target catalog courses below level 7 without options are used, so no course-option journey is silently skipped. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\ManualApprenticeDetailsPage.cs` | Confirm source personal/training field names, Save/Add transitions, and employer funding-band summary. Check current funding metadata against the existing target catalog; the assertion deliberately fails on a mismatch. Email cleanup is registered with the existing framework. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\ManualCohortConfirmationPage.cs` | Confirm source confirmation headings and reference rows. Missing/malformed references throw rather than returning success-shaped text. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\EmployerApproveLearnerDetailsPage.cs` | Declaration is made partial only; review that existing ILR behavior remains untouched. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\EmployerApproveLearnerDetailsPage.ManualEntry.cs` | Confirm source approval/send radios, message fields, counts, row prices, edit links, cohort status and confirmations. Both manual and known target heading forms are verified in this separate path. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\EditLearnerDetailsPage.cs` | Confirm source pre-approval name/DOB/reference controls and submit IDs still apply after rejection. Check edits persist through provider review and final live records. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\LearnerRequestsPage.cs` | Confirm Ready to review/With transfer sender tab IDs and cohort-specific Details links. Selection is by the created cohort, not the first row. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\ManageYourLearnersPage.cs` | New method verifies the matching learner row has status `Live`, not just that some learner is present. Verify behavior with and without the conditional search box and with both apprentices in TR_02. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\ApproveApprenticeDetailsPage.cs` | Declaration is made partial only; default ILR actions/verification are unchanged. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\ApproveApprenticeDetailsPage.ManualEntry.cs` | Confirm manual Add path, missing-ULN cells, row-specific editing and approve confirmation. The historical source binding labelled "edits the apprentice Name" actually invokes missing-ULN completion; no unsupported provider name edit is invented. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\EditLearnerDetails_ProviderPage.cs` | Confirm source `#Uln` and Save controls are available for employer-created apprentices. Existing ILR DOB/editability methods are unchanged. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\RecognitionOfPriorLearningPage.cs` | Confirm No-RPL submission returns to manual review, not an ILR-only route. Existing ILR return behavior is kept separate. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\ApprenticeRequests_ProviderPage.cs` | Confirm cohort-specific link appears under Ready for review. New orchestration reuses existing bingo-box navigation without the default fixed-delay cohort-existence method. |

## Transfers and shared employer files requiring review

| Exact repository-relative path | Concrete review reason |
|---|---|
| `src\SFA.DAS.Transfers.UITests\Project\Helpers\TransferConnectionStepsHelper.cs` | Confirm two/three-account creation and distinct company selection, legal-entity lookup and organisation-based mapping. Verify both sides of each connection. |
| `src\SFA.DAS.EmployerPortal.UITests\Project\Helpers\AccountCreationStepsHelper.cs` | Default organisation bookkeeping was corrected for additional accounts. Smoke-check another caller of the Default overload; explicit Charity/Company/etc. handling is unchanged. |
| `src\SFA.DAS.EmployerPortal.UITests\Project\Pages\TasksHomePage.cs` | Check zero, one and two connection requests; receiver review tasks; and sender transfer task presence/absence. Apprentice/learner/cohort wording and whitespace are supported; numeric counts are anchored to avoid matching 1 against 11. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Steps\CreateAccountTransfersSteps.cs` | Review the original scenario phrases, role/account switches, approval aliases and two rejection cycles. All binding work propagates Tasks. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\FinancePage.cs` | Confirm shared finance navigation and verification. Constructor navigation is disabled; the menu click is awaited explicitly. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\TransfersPage.cs` | Check source connection tables and funding-request rows. Funding approval/rejection intentionally retains source latest-pending matching by receiver and total cost. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\ConnectWithReceivingEmployerPage.cs` | Confirm source intro page and Continue link. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\WhichEmployerAreYouConnectingWithPage.cs` | Confirm receiving account public-hashed-ID field and continuation. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\ConfirmConnectionDetailsPage.cs` | Confirm source Yes radio and request-send transition. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\RequestSentConfirmPage.cs` | Confirm source h2, homepage radio and Continue behavior. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\TransferConnectionRequestDetailsPage.cs` | Confirm source accept radio and approval continuation. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\ConnectionConfirmedPage.cs` | Confirm source h2, homepage radio and Continue behavior after acceptance. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\TransferRequestDetailsPage.cs` | Confirm source funding approve/reject radios and submit control. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\TransferRequestApprovedPage.cs` | Confirm the approved request page heading. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Pages\TransferRequestRejectedPage.cs` | Confirm the rejected request page heading. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Steps\VerifyDbConnectionSteps.cs` | In a separately approved environment, check the same 21 database helpers/catalog-prefix assertions as source. Existing environment exclusions are honored, failures are aggregated and logged, and no new authentication mechanism is introduced. |
| `src\SFA.DAS.Transfers.UITests\SFA.DAS.Transfers.UITests.csproj` | Review direct TestDataCleanup reference and embedded feature resources for offline contracts. Existing Reqnroll binding-assembly configuration already supplies UI framework bindings. |
| `src\SFA.DAS.Transfers.UITests\Project\Tests\Contracts\TransfersBindingContractTests.cs` | These are offline regex-binding and async-return checks, not browser/Reqnroll-runtime acceptance tests. Update the contract if introducing scenario outlines or more complex scope rules later. |

## Feature acceptance checklist

The exact feature directory is `src\SFA.DAS.Transfers.UITests\Project\Tests\Features\`.

| Feature | Required acceptance evidence |
|---|---|
| `TR_01_TransfersApprovalJourney.feature` | Empty request includes sent message; provider adds/approves; receiver task appears then disappears; sender task appears then disappears; resulting apprentice is Live. |
| `TR_02_TransfersApprovalJourney.feature` | Two employer-created apprentices retain the selected course/prices; provider supplies both missing ULNs; sender approves; both records are Live. |
| `TR_03_TransfersApprovalJourney.feature` | Exact pending/rejected statuses; first edit persists; both rejection cycles remove sender tasks; final provider/receiver/sender approvals produce the Live record. |
| `TR_04_01_TransfersConnectionJourney.feature` through `TR_04_06_TransfersConnectionJourney.feature` | All original connection topologies, two-sided Approved checks, and task counts remain correct in a three-account test where applicable. |
| `Accessibility\TR_Accessibility.feature` | Same business journey, including legacy cohort-task wording. Source base-page behavior inspected did not establish an analyzer invocation; this migration does not claim Axe/WCAG coverage or add an analyzer package. |
| `TestDataPreparation\VerifyDbConnection.feature` | Run only with approved SQL/environment configuration. Although its step only queries catalogs, shared scenario hooks can also connect to UI/APIs/service bus. |
| `TR_05_TransfersStatusJourney.feature` and `TR_01_ProviderLogin.feature` | Existing behavior was retained; include in environment acceptance to detect shared-hook regressions. |

## Safe execution constraints

Use the repository's existing configuration injection and secret handling. Checked-in Transfers project settings contain replacement tokens, not verified usable credentials. Do not commit environment secrets to make these scenarios run.

Before any environment-connected run, confirm that account/PAYE creation, levy setup, provider/receiver/sender approvals, database access, cleanup and the existing Approvals service-bus hooks are safe and authorized. Start with one connection scenario, then each approval journey, then remaining connections/status/provider-login and the separately approved DB scenario. Capture actual page/control incompatibilities rather than suppressing failures.

Existing framework async-void navigation constructors, default provider fixed waits, compiler warnings and the NuGet advisory are pre-existing limitations, not broad refactors in this branch. New Transfers navigation is awaited and the new manual review path avoids those default provider wait methods. Existing target data factories still contain their own pre-existing short delays.

Until that environment acceptance is recorded, the delivery is an implemented and offline-validated Transfers migration with explicit runtime review gates, not a claim that the deployed application or the whole enterprise suite has passed.
