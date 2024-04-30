/* eslint-disable @typescript-eslint/no-explicit-any */
import React from 'react'

function Pagination({ pageNo = 1, setPageNo }: any) {

    const handlePrevious = () => {
        setPageNo((prevPage: number) => Math.max(prevPage - 1, 1));
    };

    const handleNext = () => {
        setPageNo((prevPage: number) => Math.min(prevPage + 1, 4));
    };

    return (
        <div className='d-flex flex-stack flex-wrap pt-10'>
            <div className='fs-6 fw-bold text-gray-700'></div>

            <ul className='pagination'>
                <li className='page-item previous' onClick={handlePrevious}>
                    <a href='#' className='page-link'>
                        <i className='previous'></i>
                    </a>
                </li>

                {[...Array(4)].map((_, index) => (
                    <li className={'page-item ' + (pageNo === index + 1 ? " active" : '')}>
                        <a href='#' className='page-link'
                            onClick={() => {
                                setPageNo(index + 1)
                            }}
                        >
                            {index + 1}
                        </a>
                    </li>

                ))}
                <li className='page-item next' onClick={handleNext}>
                    <a href='#' className='page-link'>
                        <i className='next'></i>
                    </a>
                </li>
            </ul>
        </div>
    )
}

export default Pagination