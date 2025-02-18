﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.IoT.TimeSeriesInsights
{
    /// <summary>
    /// Types client that can be used to perform operations such as creating, listing, replacing and deleting Time Series types.
    /// </summary>
    public class TypesClient
    {
        private readonly TimeSeriesTypesRestClient _typesRestClient;
        private readonly ClientDiagnostics _clientDiagnostics;

        /// <summary>
        /// Initializes a new instance of TypesClient. This constructor should only be used for mocking purposes.
        /// </summary>
        protected TypesClient()
        {
        }

        internal TypesClient(TimeSeriesTypesRestClient typesRestClient, ClientDiagnostics clientDiagnostics)
        {
            Argument.AssertNotNull(typesRestClient, nameof(typesRestClient));
            Argument.AssertNotNull(clientDiagnostics, nameof(clientDiagnostics));

            _typesRestClient = typesRestClient;
            _clientDiagnostics = clientDiagnostics;
        }

        /// <summary>
        /// Gets Time Series Insights types in pages asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The pageable list <see cref="AsyncPageable{TimeSeriesType}"/> of Time Series types with the http response.</returns>
        public virtual AsyncPageable<TimeSeriesType> GetTypesAsync(
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
            scope.Start();

            try
            {
                async Task<Page<TimeSeriesType>> FirstPageFunc(int? pageSizeHint)
                {
                    using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
                    scope.Start();

                    try
                    {
                        Response<GetTypesPage> getTypesResponse = await _typesRestClient
                            .ListAsync(null, null, cancellationToken)
                            .ConfigureAwait(false);
                        return Page.FromValues(getTypesResponse.Value.Types, getTypesResponse.Value.ContinuationToken, getTypesResponse.GetRawResponse());
                    }
                    catch (Exception ex)
                    {
                        scope.Failed(ex);
                        throw;
                    }
                }

                async Task<Page<TimeSeriesType>> NextPageFunc(string nextLink, int? pageSizeHint)
                {
                    using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
                    scope.Start();

                    try
                    {
                        Response<GetTypesPage> getTypesResponse = await _typesRestClient
                            .ListAsync(nextLink, null, cancellationToken)
                            .ConfigureAwait(false);
                        return Page.FromValues(getTypesResponse.Value.Types, getTypesResponse.Value.ContinuationToken, getTypesResponse.GetRawResponse());
                    }
                    catch (Exception ex)
                    {
                        scope.Failed(ex);
                        throw;
                    }
                }

                return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets Time Series Insights types in pages synchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The pageable list <see cref="Pageable{TimeSeriesType}"/> of Time Series types with the http response.</returns>
        /// <seealso cref="GetTypesAsync(CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        public virtual Pageable<TimeSeriesType> GetTypes(CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
            scope.Start();

            try
            {
                Page<TimeSeriesType> FirstPageFunc(int? pageSizeHint)
                {
                    using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
                    scope.Start();

                    try
                    {
                        Response<GetTypesPage> getTypesResponse = _typesRestClient.List(null, null, cancellationToken);
                        return Page.FromValues(getTypesResponse.Value.Types, getTypesResponse.Value.ContinuationToken, getTypesResponse.GetRawResponse());
                    }
                    catch (Exception ex)
                    {
                        scope.Failed(ex);
                        throw;
                    }
                }

                Page<TimeSeriesType> NextPageFunc(string nextLink, int? pageSizeHint)
                {
                    using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetTypes)}");
                    scope.Start();

                    try
                    {
                        Response<GetTypesPage> getTypesResponse = _typesRestClient.List(nextLink, null, cancellationToken);
                        return Page.FromValues(getTypesResponse.Value.Types, getTypesResponse.Value.ContinuationToken, getTypesResponse.GetRawResponse());
                    }
                    catch (Exception ex)
                    {
                        scope.Failed(ex);
                        throw;
                    }
                }

                return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets Time Series Insights types by type names asynchronously.
        /// </summary>
        /// <param name="timeSeriesTypeNames">List of names of the Time Series types to return.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of type or error objects corresponding by position to the array in the request.
        /// Type object is set when operation is successful and error object is set when operation is unsuccessful.
        /// </returns>
        /// <remarks>
        /// For more samples, see <see href="https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/timeseriesinsights/Azure.IoT.TimeSeriesInsights/samples">our repo samples</see>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is empty.
        /// </exception>
        public virtual async Task<Response<TimeSeriesTypeOperationResult[]>> GetByNameAsync(
            IEnumerable<string> timeSeriesTypeNames,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetByName)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeNames, nameof(timeSeriesTypeNames));

                var batchRequest = new TypesBatchRequest()
                {
                    Get = new TypesRequestBatchGetOrDelete()
                };

                foreach (string timeSeriesName in timeSeriesTypeNames)
                {
                    batchRequest.Get.Names.Add(timeSeriesName);
                }

                Response<TypesBatchResponse> executeBatchResponse = await _typesRestClient
                    .ExecuteBatchAsync(batchRequest, null, cancellationToken)
                    .ConfigureAwait(false);

                return Response.FromValue(executeBatchResponse.Value.Get.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets Time Series Insights types by type names synchronously.
        /// </summary>
        /// <param name="timeSeriesTypeNames">List of names of the Time Series types to return.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of type or error objects corresponding by position to the array in the request.
        /// Type object is set when operation is successful and error object is set when operation is unsuccessful.
        /// </returns>
        /// <seealso cref="GetByNameAsync(IEnumerable{string}, CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is empty.
        /// </exception>
        public virtual Response<TimeSeriesTypeOperationResult[]> GetByName(
            IEnumerable<string> timeSeriesTypeNames,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetByName)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeNames, nameof(timeSeriesTypeNames));

                var batchRequest = new TypesBatchRequest()
                {
                    Get = new TypesRequestBatchGetOrDelete()
                };

                foreach (string timeSeriesName in timeSeriesTypeNames)
                {
                    batchRequest.Get.Names.Add(timeSeriesName);
                }

                Response<TypesBatchResponse> executeBatchResponse = _typesRestClient
                    .ExecuteBatch(batchRequest, null, cancellationToken);

                return Response.FromValue(executeBatchResponse.Value.Get.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets Time Series Insights types by type Ids asynchronously.
        /// </summary>
        /// <param name="timeSeriesTypeIds">List of Time Series type Ids of the Time Series types to return.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of type or error objects corresponding by position to the array in the request.
        /// Type object is set when operation is successful and error object is set when operation is unsuccessful.
        /// </returns>
        /// <remarks>
        /// For more samples, see <see href="https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/timeseriesinsights/Azure.IoT.TimeSeriesInsights/samples">our repo samples</see>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is empty.
        /// </exception>
        public virtual async Task<Response<TimeSeriesTypeOperationResult[]>> GetByIdAsync(
            IEnumerable<string> timeSeriesTypeIds,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetById)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeIds, nameof(timeSeriesTypeIds));

                var batchRequest = new TypesBatchRequest()
                {
                    Get = new TypesRequestBatchGetOrDelete()
                };

                foreach (string typeId in timeSeriesTypeIds)
                {
                    batchRequest.Get.TypeIds.Add(typeId);
                }

                Response<TypesBatchResponse> executeBatchResponse = await _typesRestClient
                    .ExecuteBatchAsync(batchRequest, null, cancellationToken)
                    .ConfigureAwait(false);

                return Response.FromValue(executeBatchResponse.Value.Get.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Gets Time Series Insights types by type Ids synchronously.
        /// </summary>
        /// <param name="timeSeriesTypeIds">List of Time Series type Ids of the Time Series types to return.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of type or error objects corresponding by position to the array in the request.
        /// Type object is set when operation is successful and error object is set when operation is unsuccessful.
        /// </returns>
        /// <seealso cref="GetByIdAsync(IEnumerable{string}, CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is empty.
        /// </exception>
        public virtual Response<TimeSeriesTypeOperationResult[]> GetById(
            IEnumerable<string> timeSeriesTypeIds,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(GetById)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeIds, nameof(timeSeriesTypeIds));

                var batchRequest = new TypesBatchRequest()
                {
                    Get = new TypesRequestBatchGetOrDelete()
                };

                foreach (string typeId in timeSeriesTypeIds)
                {
                    batchRequest.Get.TypeIds.Add(typeId);
                }

                Response<TypesBatchResponse> executeBatchResponse = _typesRestClient
                    .ExecuteBatch(batchRequest, null, cancellationToken);

                return Response.FromValue(executeBatchResponse.Value.Get.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Creates Time Series instances types asynchronously. If a provided instance type is already in use, then this will attempt to replace the existing instance type with the provided Time Series Instance.
        /// </summary>
        /// <param name="timeSeriesTypes">The Time Series instances types to be created or replaced.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of error objects corresponding by position to the <paramref name="timeSeriesTypes"/> array in the request.
        /// An error object will be set when operation is unsuccessful.
        /// </returns>
        /// <remarks>
        /// For more samples, see <see href="https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/timeseriesinsights/Azure.IoT.TimeSeriesInsights/samples">our repo samples</see>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypes"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypes"/> is empty.
        /// </exception>
        public virtual async Task<Response<TimeSeriesTypeOperationResult[]>> CreateOrReplaceAsync(
            IEnumerable<TimeSeriesType> timeSeriesTypes,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics
                .CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(CreateOrReplace)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypes, nameof(timeSeriesTypes));

                var batchRequest = new TypesBatchRequest();

                foreach (TimeSeriesType type in timeSeriesTypes)
                {
                    batchRequest.Put.Add(type);
                }

                Response<TypesBatchResponse> executeBatchResponse = await _typesRestClient
                    .ExecuteBatchAsync(batchRequest, null, cancellationToken)
                    .ConfigureAwait(false);

                IEnumerable<TimeSeriesOperationError> errorResults = executeBatchResponse.Value.Put.Select((result) => result.Error);

                return Response.FromValue(executeBatchResponse.Value.Put.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Creates Time Series instances types asynchronously. If a provided instance type is already in use, then this will attempt to replace the existing instance type with the provided Time Series Instance.
        /// </summary>
        /// <param name="timeSeriesTypes">The Time Series instances types to be created or replaced.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of types or error objects corresponding by position to the <paramref name="timeSeriesTypes"/> array in the request.
        /// Type object is set when operation is successful and error object is set when operation is unsuccessful.
        /// </returns>
        /// <seealso cref="CreateOrReplaceAsync(IEnumerable{TimeSeriesType}, CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypes"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypes"/> is empty.
        /// </exception>
        public virtual Response<TimeSeriesTypeOperationResult[]> CreateOrReplace(
            IEnumerable<TimeSeriesType> timeSeriesTypes,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(CreateOrReplace)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypes, nameof(timeSeriesTypes));

                var batchRequest = new TypesBatchRequest();

                foreach (TimeSeriesType type in timeSeriesTypes)
                {
                    batchRequest.Put.Add(type);
                }

                Response<TypesBatchResponse> executeBatchResponse = _typesRestClient
                    .ExecuteBatch(batchRequest, null, cancellationToken);

                IEnumerable<TimeSeriesOperationError> errorResults = executeBatchResponse.Value.Put.Select((result) => result.Error);

                return Response.FromValue(executeBatchResponse.Value.Put.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes Time Series Insights types by type names asynchronously.
        /// </summary>
        /// <param name="timeSeriesTypeNames">List of names of the Time Series types to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of error objects corresponding by position to the input array in the request. null when the operation is successful.
        /// </returns>
        /// <remarks>
        /// For more samples, see <see href="https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/timeseriesinsights/Azure.IoT.TimeSeriesInsights/samples">our repo samples</see>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is empty.
        /// </exception>
        public virtual async Task<Response<TimeSeriesOperationError[]>> DeleteByNameAsync(
            IEnumerable<string> timeSeriesTypeNames,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(DeleteByName)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeNames, nameof(timeSeriesTypeNames));

                var batchRequest = new TypesBatchRequest
                {
                    Delete = new TypesRequestBatchGetOrDelete()
                };

                foreach (string timeSeriesName in timeSeriesTypeNames)
                {
                    batchRequest.Delete.Names.Add(timeSeriesName);
                }

                Response<TypesBatchResponse> executeBatchResponse = await _typesRestClient
                    .ExecuteBatchAsync(batchRequest, null, cancellationToken)
                    .ConfigureAwait(false);

                return Response.FromValue(executeBatchResponse.Value.Delete.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes Time Series Insights types by type names asynchronously.
        /// </summary>
        /// <param name="timeSeriesTypeNames">List of names of the Time Series types to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of error objects corresponding by position to the input array in the request. null when the operation is successful.
        /// </returns>
        /// <seealso cref="DeleteByNameAsync(IEnumerable{string}, CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeNames"/> is empty.
        /// </exception>
        public virtual Response<TimeSeriesOperationError[]> DeleteByName(
            IEnumerable<string> timeSeriesTypeNames,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(DeleteByName)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeNames, nameof(timeSeriesTypeNames));

                var batchRequest = new TypesBatchRequest
                {
                    Delete = new TypesRequestBatchGetOrDelete()
                };

                foreach (string timeSeriesName in timeSeriesTypeNames)
                {
                    batchRequest.Delete.Names.Add(timeSeriesName);
                }

                Response<TypesBatchResponse> executeBatchResponse = _typesRestClient
                    .ExecuteBatch(batchRequest, null, cancellationToken);

                return Response.FromValue(executeBatchResponse.Value.Delete.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes Time Series Insights types by type Ids asynchronously.
        /// </summary>
        /// <param name="timeSeriesTypeIds">List of Time Series type Ids of the Time Series types to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of error objects corresponding by position to the input array in the request. null when the operation is successful.
        /// </returns>
        /// <remarks>
        /// For more samples, see <see href="https://github.com/Azure/azure-sdk-for-net/tree/master/sdk/timeseriesinsights/Azure.IoT.TimeSeriesInsights/samples">our repo samples</see>.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is empty.
        /// </exception>
        public virtual async Task<Response<TimeSeriesOperationError[]>> DeleteByIdAsync(
            IEnumerable<string> timeSeriesTypeIds,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(DeleteById)}");
            scope.Start();

            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeIds, nameof(timeSeriesTypeIds));

                var batchRequest = new TypesBatchRequest
                {
                    Delete = new TypesRequestBatchGetOrDelete()
                };

                foreach (string typeId in timeSeriesTypeIds)
                {
                    batchRequest.Delete.TypeIds.Add(typeId);
                }

                Response<TypesBatchResponse> executeBatchResponse = await _typesRestClient
                    .ExecuteBatchAsync(batchRequest, null, cancellationToken)
                    .ConfigureAwait(false);

                return Response.FromValue(executeBatchResponse.Value.Delete.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes Time Series instances from the environment by Time Series Ids synchronously.
        /// </summary>
        /// <param name="timeSeriesTypeIds">List of Ids of the Time Series instances to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// List of error objects corresponding by position to the input array in the request. null when the operation is successful.
        /// </returns>
        /// <seealso cref="DeleteByIdAsync(IEnumerable{string}, CancellationToken)">
        /// See the asynchronous version of this method for examples.
        /// </seealso>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The exception is thrown when <paramref name="timeSeriesTypeIds"/> is empty.
        /// </exception>
        public virtual Response<TimeSeriesOperationError[]> DeleteById(
            IEnumerable<string> timeSeriesTypeIds,
            CancellationToken cancellationToken = default)
        {
            using DiagnosticScope scope = _clientDiagnostics.CreateScope($"{nameof(TimeSeriesInsightsClient)}.{nameof(DeleteById)}");
            scope.Start();
            try
            {
                Argument.AssertNotNullOrEmpty(timeSeriesTypeIds, nameof(timeSeriesTypeIds));

                var batchRequest = new TypesBatchRequest
                {
                    Delete = new TypesRequestBatchGetOrDelete()
                };

                foreach (string typeId in timeSeriesTypeIds ?? Enumerable.Empty<string>())
                {
                    batchRequest.Delete.TypeIds.Add(typeId);
                }
                Response<TypesBatchResponse> executeBatchResponse = _typesRestClient
                    .ExecuteBatch(batchRequest, null, cancellationToken);
                return Response.FromValue(executeBatchResponse.Value.Delete.ToArray(), executeBatchResponse.GetRawResponse());
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }
    }
}
