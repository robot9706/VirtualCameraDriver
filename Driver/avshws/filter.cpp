/**************************************************************************

    AVStream Simulated Hardware Sample

    Copyright (c) 2001, Microsoft Corporation.

    File:

        filter.cpp

    Abstract:

        This file contains the filter level implementation for the 
        capture filter.

    History:

        created 3/12/2001

**************************************************************************/

#include "avshws.h"

/**************************************************************************

    PAGEABLE CODE

**************************************************************************/

#ifdef ALLOC_PRAGMA
#pragma code_seg("PAGE")
#endif // ALLOC_PRAGMA


NTSTATUS
CCaptureFilter::
DispatchCreate (
    IN PKSFILTER Filter,
    IN PIRP Irp
    )

/*++

Routine Description:

    This is the creation dispatch for the capture filter.  It creates
    the CCaptureFilter object, associates it with the AVStream filter
    object, and bag the CCaptureFilter for later cleanup.

Arguments:

    Filter -
        The AVStream filter being created

    Irp -
        The creation Irp

Return Value:
    
    Success / failure

--*/

{

    PAGED_CODE();

    NTSTATUS Status = STATUS_SUCCESS;

    CCaptureFilter *CapFilter = new (NonPagedPoolNx, 'liFC') CCaptureFilter (Filter);

    if (!CapFilter) {
        //
        // Return failure if we couldn't create the filter.
        //
        Status = STATUS_INSUFFICIENT_RESOURCES;

    } else {
        //
        // Add the item to the object bag if we we were successful. 
        // Whenever the filter closes, the bag is cleaned up and we will be
        // freed.
        //
        Status = KsAddItemToObjectBag (
            Filter -> Bag,
            reinterpret_cast <PVOID> (CapFilter),
            reinterpret_cast <PFNKSFREE> (CCaptureFilter::Cleanup)
            );

        if (!NT_SUCCESS (Status)) {
            delete CapFilter;
        } else {
            Filter -> Context = reinterpret_cast <PVOID> (CapFilter);
        }

    }

    return Status;

}

//  Get KSPROPERTY_CUSTOMCONTROL_DUMMY.
NTSTATUS
CCaptureFilter::
GetData(
	_In_ PIRP Irp,
	_In_ PKSIDENTIFIER Request,
	_Inout_ PVOID Data
)
{
	PAGED_CODE();

	PDWORD dataPtr = (PDWORD)Data;

	*dataPtr = 0xAA77AA77;
	Irp->IoStatus.Information = sizeof(DWORD);

	return STATUS_SUCCESS;
}

//  Set KSPROPERTY_CUSTOMCONTROL_DUMMY.
NTSTATUS
CCaptureFilter::
SetData(
	_In_ PIRP Irp,
	_In_ PKSIDENTIFIER Request,
	_Inout_ PVOID Data
)
{
	PAGED_CODE();

	CCaptureFilter* filter = reinterpret_cast<CCaptureFilter*>(KsGetFilterFromIrp(Irp)->Context);

	PIO_STACK_LOCATION pIrpStack = IoGetCurrentIrpStackLocation(Irp);
	ULONG bufferLength = pIrpStack->Parameters.DeviceIoControl.OutputBufferLength;

	if (bufferLength == 0 || Data == NULL) {
		return STATUS_SUCCESS;
	}

	CCaptureDevice* device = CCaptureDevice::Recast(KsFilterGetDevice(filter->m_Filter));
	device->SetData(Data, bufferLength);

	return STATUS_SUCCESS;
}

//  Get KSPROPERTY_CUSTOMCONTROL_DUMMY.
NTSTATUS
CCaptureFilter::
GetState(
    _In_ PIRP Irp,
    _In_ PKSIDENTIFIER Request,
    _Inout_ PVOID Data
)
{
    PAGED_CODE();

    CCaptureFilter* filter = reinterpret_cast<CCaptureFilter*>(KsGetFilterFromIrp(Irp)->Context);

    PIO_STACK_LOCATION pIrpStack = IoGetCurrentIrpStackLocation(Irp);
    ULONG bufferLength = pIrpStack->Parameters.DeviceIoControl.OutputBufferLength;

    if (bufferLength == 0 || Data == NULL) {
        return STATUS_SUCCESS;
    }

    CCaptureDevice* device = CCaptureDevice::Recast(KsFilterGetDevice(filter->m_Filter));
    DWORD state = device->GetState();
    PDWORD dataPtr = (PDWORD)Data;

    *dataPtr = state;
    Irp->IoStatus.Information = sizeof(DWORD);

    return STATUS_SUCCESS;
}

//  Set KSPROPERTY_CUSTOMCONTROL_DUMMY.
NTSTATUS
CCaptureFilter::
SetState(
    _In_ PIRP Irp,
    _In_ PKSIDENTIFIER Request,
    _Inout_ PVOID Data
)
{
    PAGED_CODE();

    return STATUS_SUCCESS;
}

/**************************************************************************

	PROPERTY TABLE STUFF

**************************************************************************/

DEFINE_KSPROPERTY_TABLE(CustomPropertyTable)
{
	{
		0,											//PropertyId
		(PFNKSHANDLER)&CCaptureFilter::GetData,		//GetPropertyHandler
		(ULONG)sizeof(KSPROPERTY),					//MinProperty
		(ULONG)0,								//MinData
		(PFNKSHANDLER)&CCaptureFilter::SetData,		//SetPropertyHandler
		(PKSPROPERTY_VALUES)NULL,					//Values
		0,											//RelationsCount
		(PKSPROPERTY)NULL,							//Relations
		(PFNKSHANDLER)NULL,							//SupportHandler
		(ULONG)0									//SerializedSize
	},
    {
        1,											//PropertyId
        (PFNKSHANDLER)&CCaptureFilter::GetState,    //GetPropertyHandler
        (ULONG)sizeof(KSPROPERTY),					//MinProperty
        (ULONG)0,								    //MinData
        (PFNKSHANDLER)&CCaptureFilter::SetState,	//SetPropertyHandler
        (PKSPROPERTY_VALUES)NULL,					//Values
        0,											//RelationsCount
        (PKSPROPERTY)NULL,							//Relations
        (PFNKSHANDLER)NULL,							//SupportHandler
        (ULONG)0									//SerializedSize
    }
};

DEFINE_KSPROPERTY_SET_TABLE(PropertySetTable)
{
	DEFINE_STD_PROPERTY_SET(PROPSETID_VIDCAP_CUSTOMCONTROL, CustomPropertyTable)
};


DEFINE_KSAUTOMATION_TABLE(AvsFilterAutomationTable)
{
	DEFINE_KSAUTOMATION_PROPERTIES(PropertySetTable),
	DEFINE_KSAUTOMATION_METHODS_NULL,
	DEFINE_KSAUTOMATION_EVENTS_NULL
};

/**************************************************************************

    DESCRIPTOR AND DISPATCH LAYOUT

**************************************************************************/

GUID g_PINNAME_VIDEO_CAPTURE = {STATIC_PINNAME_VIDEO_CAPTURE};

//
// CaptureFilterCategories:
//
// The list of category GUIDs for the capture filter.
//
const
GUID
CaptureFilterCategories [CAPTURE_FILTER_CATEGORIES_COUNT] = {
    STATICGUIDOF (KSCATEGORY_VIDEO),
    STATICGUIDOF (KSCATEGORY_CAPTURE),
    STATICGUIDOF (KSCATEGORY_VIDEO_CAMERA)
};

//
// CaptureFilterPinDescriptors:
//
// The list of pin descriptors on the capture filter.  
//
const 
KSPIN_DESCRIPTOR_EX
CaptureFilterPinDescriptors [CAPTURE_FILTER_PIN_COUNT] = {
    //
    // Video Capture Pin
    //
    {
        &CapturePinDispatch,
        NULL,             
        {
            0,                              // Interfaces (NULL, 0 == default)
            NULL,
            0,                              // Mediums (NULL, 0 == default)
            NULL,
            SIZEOF_ARRAY(CapturePinDataRanges),// Range Count
            CapturePinDataRanges,           // Ranges
            KSPIN_DATAFLOW_OUT,             // Dataflow
            KSPIN_COMMUNICATION_BOTH,       // Communication
            &PIN_CATEGORY_CAPTURE,          // Category
            &g_PINNAME_VIDEO_CAPTURE,       // Name
            0                               // Reserved
        },
        KSPIN_FLAG_PROCESS_IN_RUN_STATE_ONLY,// Pin Flags
        1,                                  // Instances Possible
        1,                                  // Instances Necessary
        &CapturePinAllocatorFraming,        // Allocator Framing
        reinterpret_cast <PFNKSINTERSECTHANDLEREX> 
            (CCapturePin::IntersectHandler)
    }
};

//
// CaptureFilterDispatch:
//
// This is the dispatch table for the capture filter.  It provides notification
// of creation, closure, processing (for filter-centrics, not for the capture
// filter), and resets (for filter-centrics, not for the capture filter).
//
const 
KSFILTER_DISPATCH
CaptureFilterDispatch = {
    CCaptureFilter::DispatchCreate,         // Filter Create
    NULL,                                   // Filter Close
    NULL,                                   // Filter Process
    NULL                                    // Filter Reset
};


//
// CaptureFilterDescription:
//
// The descriptor for the capture filter.  We don't specify any topology
// since there's only one pin on the filter.  Realistically, there would
// be some topological relationships here because there would be input 
// pins from crossbars and the like.
//
const 
KSFILTER_DESCRIPTOR 
CaptureFilterDescriptor = {
    &CaptureFilterDispatch,                 // Dispatch Table
    &AvsFilterAutomationTable,              // Automation Table
    KSFILTER_DESCRIPTOR_VERSION,            // Version
    0,                                      // Flags
    &KSNAME_Filter,                         // Reference GUID
    DEFINE_KSFILTER_PIN_DESCRIPTORS (CaptureFilterPinDescriptors),
    DEFINE_KSFILTER_CATEGORIES (CaptureFilterCategories),
    0,
    sizeof (KSNODE_DESCRIPTOR),
    NULL,
    0,
    NULL,
    NULL                                    // Component ID
};