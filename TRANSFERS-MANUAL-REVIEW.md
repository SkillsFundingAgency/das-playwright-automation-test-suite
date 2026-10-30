# Transfers migration: manual review and acceptance gates

Implementation summary: [TRANSFERS-MIGRATION.md](TRANSFERS-MIGRATION.md).
Branch: `gpeterthomas-selenium-to-playwright-migration`.

The source scenarios, their bindings and needed page interactions have been implemented and compile. The following files still require review against a known-safe deployed environment. No live transfer, account creation, database catalog query, approval or service-bus flow was executed. A build or binding-discovery pass must not be interpreted as end-to-end acceptance.

## ILR journey acceptance gates

The three approval journeys and the tagged accessibility journey now use the target's existing ILR approval flow. The receiver-specific `Employer` data, including its agreement and legal-entity IDs, is used to generate and publish learners before the empty transfer-funded cohort is created. The provider then selects those learners from ILR and uses the existing approval pages. This migration does not add another ILR event pipeline or replace the target's default approval behavior.

The source TR_03 receiver-edit action is not available for ILR-backed learner data in the target approval UI. It is adapted to the supported action of requesting provider review of the rejected cohort; the ILR learner itself is not edited. Confirm this distinction is acceptable in the deployed workflow. Do not report the source learner edit as preserved.

The ILR provider-add route returns the cohort to the receiver for approval before it becomes ready for sender approval. TR_02 and the TR_03 cycles therefore include explicit receiver approval steps that were implicit in the source manual-entry creation route. Verify those task/status transitions in the deployed application.

## Shared approval files requiring review

| Exact repository-relative path | Concrete review reason |
|---|---|
| `src\SFA.DAS.Approvals.UITests\Project\Helpers\StepsHelper\TransferCohortStepsHelper.cs` | Verify receiver/provider role switching, cohort references, task/status transitions, both sender rejection cycles and the final live-record checks. Check that the actual receiver account stays paired to the ILR learner and transfer cohort. |
| `src\SFA.DAS.Approvals.UITests\Project\Helpers\TestDataHelpers\ApprenticeshipModel\ApprenticeDataHelper.cs` | Review the explicit-employer overload and `WaitingToStart` status parameter; existing employer-type overloads remain in use by other tests. |
| `src\SFA.DAS.Approvals.UITests\Project\Steps\LearnerDataOuterApiSteps.cs` | Confirm generated receiver learners are published through the existing standard ILR event path and consumed by the configured service-bus hooks. |
| `src\SFA.DAS.Approvals.UITests\Project\Helpers\StepsHelper\ProviderStepsHelper.cs` | Confirm existing first-learner/RPL handling and subsequent ILR learner selection operate for an employer-created empty cohort. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\ChooseFundingPage.cs` | Confirm direct-transfer option `#FundingType-1` still leads to connection selection. Existing ILR/reservation methods are unchanged. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\SelectTransferConnectionPage.cs` | Confirm the sender's organisation is the radio's accessible name and the specified sender, rather than an arbitrary first connection, is selected. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\HowWouldYouLikeToAddLearnersPage.cs` | Confirm the existing provider-add route still creates an empty request after direct transfer funding. Manual-entry-only methods were removed. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\EmployerApproveLearnerDetailsPage.cs` | Confirm standard ILR verification/final approval remains intact and the receiver's request-changes action submits correctly. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\LearnerRequestsPage.cs` | Confirm Ready to review/With transfer sender tabs and the new cohort-specific Details opener for ILR review. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Employer\ManageYourLearnersPage.cs` | Verify the matching ILR learner row has status `Live`, including both apprentices in TR_02. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\ApproveApprenticeDetailsPage.cs` | Confirm the unchanged default ILR selection, RPL and provider-approval controls work on transfer-funded cohorts. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\RecognitionOfPriorLearningPage.cs` | Confirm the existing ILR RPL path is reached for the first selected learner when needed. |
| `src\SFA.DAS.Approvals.UITests\Project\Pages\Provider\ApprenticeRequests_ProviderPage.cs` | Confirm the cohort-specific link is available to the existing `ProviderOpenTheCohort` helper after creation and after a receiver requests review. |

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
| `TR_01_TransfersApprovalJourney.feature` | Receiver ILR data is selected into a transfer-funded empty cohort; receiver task appears then disappears; sender task appears then disappears; learner is Live. |
| `TR_02_TransfersApprovalJourney.feature` | Both receiver ILR learners are selected into the empty cohort; receiver approves before sender approval; both records are Live. |
| `TR_03_TransfersApprovalJourney.feature` | Exact pending/rejected statuses; receiver requests provider review without editing the authoritative ILR learner; both rejection cycles remove sender tasks; receiver re-approvals precede sender approval; final live record is present. |
| `TR_04_01_TransfersConnectionJourney.feature` through `TR_04_06_TransfersConnectionJourney.feature` | All original connection topologies, two-sided Approved checks, and task counts remain correct in a three-account test where applicable. |
| `Accessibility\TR_Accessibility.feature` | Same business journey, including legacy cohort-task wording. Source base-page behavior inspected did not establish an analyzer invocation; this migration does not claim Axe/WCAG coverage or add an analyzer package. |
| `TestDataPreparation\VerifyDbConnection.feature` | Run only with approved SQL/environment configuration. Although its step only queries catalogs, shared scenario hooks can also connect to UI/APIs/service bus. |
| `TR_05_TransfersStatusJourney.feature` and `TR_01_ProviderLogin.feature` | Existing behavior was retained; include in environment acceptance to detect shared-hook regressions. |

## Safe execution constraints

Use the repository's existing configuration injection and secret handling. Checked-in Transfers project settings contain replacement tokens, not verified usable credentials. Do not commit environment secrets to make these scenarios run.

Before any environment-connected run, confirm that account/PAYE creation, levy setup, provider/receiver/sender approvals, database access, cleanup and the existing Approvals service-bus hooks are safe and authorized. Start with one connection scenario, then each approval journey, then remaining connections/status/provider-login and the separately approved DB scenario. Capture actual page/control incompatibilities rather than suppressing failures.

Existing framework async-void navigation constructors, default provider fixed waits, compiler warnings and the NuGet advisory are pre-existing limitations, not broad refactors in this branch. The ILR revision uses existing target navigation and selection helpers and does not add fixed UI sleeps.

Until that environment acceptance is recorded, the delivery is an implemented and offline-validated Transfers migration with explicit runtime review gates, not a claim that the deployed application or the whole enterprise suite has passed.
